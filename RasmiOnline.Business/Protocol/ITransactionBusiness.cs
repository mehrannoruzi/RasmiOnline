namespace RasmiOnline.Business.Protocol
{
    using Domain.Entity;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using System;
    using System.Collections.Generic;

    public interface ITransactionBusiness
    {
        IEnumerable<TransactionDetails> GetDetail(int orderId);
        IActionResponse<Transaction> Do(Transaction model);
        IActionResponse<int> Update(Transaction model);
        Transaction Find(string authority);
        Transaction Find(int transactionId);
        IActionResponse<int> Insert(Transaction model);
        int AllUnReadPaymentCount();
        IEnumerable<Transaction> AllUnReadPayment();
        IActionResponse<int> Read(int transactionId,bool isOffice);
        int AllUnReadOfficePaymentCount(Guid officeUserId);
        IEnumerable<Transaction> AllUnReadOfficePayment(Guid officeUserId);
        IActionResponse<int> Remove(int orderId, int transactionId);

        int GetTotalPayedPrice(int orderId);
    }
}