using System.Collections.Generic;

namespace DesafioVerity.Domain.Entity
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Extract> Extracts { get; set; }

        public enum eTransactionType
        {
            Credito = 1,
            Debito = 2
        }

    }

}
