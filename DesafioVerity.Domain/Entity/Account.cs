using System;

namespace DesafioVerity.Domain.Entity
{
    public class Account
    {
        public int AccountHolderId { get; set; }
        public decimal Value { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual AccountHolder AccountHolder { get; set; }
    }
}
