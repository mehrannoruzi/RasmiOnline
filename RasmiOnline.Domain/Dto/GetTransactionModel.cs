namespace RasmiOnline.Domain.Dto
{
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;

    public class GetTransactionModel
    {
        public bool Removeable { get; set; }
        public int OrderId { get; set; }
        public IEnumerable<TransactionDetails> Transactions { get; set; }
    }
}
