using Braintree;
using SkyDrive.Payment.Services.Models;

namespace SkyDrive.Payment.Services.Abstractions
{
    public interface IBraintreeService
    {
        public Task<IEnumerable<Transaction>> GetAllTransactions();
        public Task<IEnumerable<Transaction>> GetTransactionsByCustomerId(string id);
        public Task<Transaction> GetTransactionById(string id);
        public Task CreateTransaction(TransactionInfo transactionInfo);
    }
}
