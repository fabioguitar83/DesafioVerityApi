using DesafioVerity.Domain.Common;
using DesafioVerity.Domain.Entity;
using DesafioVerity.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using static DesafioVerity.Domain.Entity.TransactionType;

namespace DesafioVerity.Repository.Initialize
{
    public static class InicializaBD
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            if (context.TransactionType.Count() <= 0)
            {
                context.TransactionType.Add(new Domain.Entity.TransactionType() { Id = 1, Description = "Crédito" });
                context.TransactionType.Add(new Domain.Entity.TransactionType() { Id = 2, Description = "Débito" });
            }

            if (context.AccountHolder.Count() <= 0)
            {

                var accountHolder = new AccountHolder();

                accountHolder.Name = "Fabio Guedes";
                accountHolder.Password = EncryptPassword.Encrypt("123456");
                accountHolder.Agency = 1;
                accountHolder.AccountNumber = 1234;
                accountHolder.CreateDate = DateTime.Now;

                accountHolder.Account = new Account()
                {
                    UpdateDate = DateTime.Now,
                    Value = 1000
                };

                accountHolder.Extracts = new List<Extract>();

                accountHolder.Extracts.Add(new Extract()
                {
                    TransactionTypeId = eTransactionType.Credito.GetHashCode(),
                    Value = 1000,
                    TransactionDate = DateTime.Now
                });


                context.AccountHolder.Add(accountHolder);
            }


            context.SaveChanges();
        }
    }
}
