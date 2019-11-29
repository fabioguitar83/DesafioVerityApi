using System;
using System.Collections.Generic;

namespace DesafioVerity.Domain.Entity
{
    public class AccountHolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Agency { get; set; }
        public int AccountNumber { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Extract> Extracts { get; set; }

    }
}
