using System;

namespace DesafioVerity.Domain.Entity
{
    public class Extract
    {
        public int Id { get; set; }
        public int AccountHolderId { get; set; }
        public decimal Value { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionTypeId { get; set; }

        public virtual AccountHolder AccountHolder { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
