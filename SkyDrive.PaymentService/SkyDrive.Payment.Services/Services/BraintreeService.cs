using Braintree;
using Mapster;
using Microsoft.Extensions.Configuration;
using SkyDrive.Payment.Services.Abstractions;
using SkyDrive.Payment.Services.Exceptions;
using SkyDrive.Payment.Services.Models;

namespace SkyDrive.Payment.Services.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration _configuration;
        private readonly IBraintreeGateway _gateway;

        public BraintreeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _gateway = CreateGateway();
        }

        private IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _configuration["BraintreeGateway:MerchantId"],
                PublicKey = _configuration["BraintreeGateway:PublicKey"],
                PrivateKey = _configuration["BraintreeGateway:PrivateKey"]
            };

            return newGateway;
        }

        private static CustomerRequest CreateCustomerRequest(TransactionInfo transactionInfo)
        {
            var request = transactionInfo.Adapt<CustomerRequest>();

            request.PaymentMethodNonce = Braintree.Test.Nonce.Transactable;
            request.CreditCard.Number = TestCardNumbers.ValidCardMaestro;

            return request;
        }

        public async Task CreateTransaction(TransactionInfo transactionInfo)
        {
            var customerRequest = CreateCustomerRequest(transactionInfo);
            var customerResult = await _gateway.Customer.CreateAsync(customerRequest);

            CheckIfResultSuccess(customerResult);

            var paymentMethodToken = customerResult.Target.PaymentMethods[0].Token;
            var nonceResult = _gateway.PaymentMethodNonce.Create(paymentMethodToken);

            CheckIfResultSuccess(nonceResult);

            var transactionRequest = CreateTransactionRequest(transactionInfo, nonceResult.Target);
            var transactionResult = await _gateway.Transaction.SaleAsync(transactionRequest);

            CheckIfResultSuccess(transactionResult);
        }

        private static TransactionRequest CreateTransactionRequest(TransactionInfo transactionInfo, PaymentMethodNonce paymentMethodNonce)
        {
            var transactionRequest = new TransactionRequest
            {
                Amount = transactionInfo.Price,
                PaymentMethodNonce = paymentMethodNonce.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            return transactionRequest;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            var request = new TransactionSearchRequest();

            ResourceCollection<Transaction> collection = await _gateway.Transaction.SearchAsync(request);

            return collection;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCustomerId(string id)
        {
            var request = new TransactionSearchRequest().CustomerId.Is(id);

            ResourceCollection<Transaction> collection = await _gateway.Transaction.SearchAsync(request);

            return collection;
        }

        public async Task<Transaction> GetTransactionById(string id)
        {
            var collection = await _gateway.Transaction.FindAsync(id);

            return collection;
        }

        private static void CheckIfResultSuccess<T>(Result<T> result) where T : class
        {
            if (!result.IsSuccess())
                throw new ResultException($"{typeof(T)} creating failed");
        }
    }
}