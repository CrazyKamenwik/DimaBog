using Braintree;
using Microsoft.AspNetCore.Mvc;
using SkyDrive.Payment.Services.Abstractions;
using SkyDrive.Payment.Services.Models;

namespace SkyDrive.PaymentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IBraintreeService _braintreeService;

        public TransactionController(IBraintreeService braintreeService)
        {
            _braintreeService = braintreeService;
        }

        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _braintreeService.GetAllTransactions();
        }

        [HttpGet("/customer/{id}")]
        public async Task<IEnumerable<Transaction>> GetTransactionsByCustomerId(string id)
        {
            return await _braintreeService.GetTransactionsByCustomerId(id);
        }

        [HttpGet("/{id}")]
        public async Task<Transaction> GetTransactionById(string id)
        {
            return await _braintreeService.GetTransactionById(id);
        }

        [HttpPost]
        public async Task CreateTransaction(TransactionInfo transactionInfo)
        {
            await _braintreeService.CreateTransaction(transactionInfo);
        }
    }
}