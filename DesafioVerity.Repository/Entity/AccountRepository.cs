using DesafioVerity.Domain.Entity;
using DesafioVerity.Domain.Interfaces.Entity;
using DesafioVerity.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DesafioVerity.Repository.Entity
{
    public class AccountRepository : Common.Repository, IAccountRepository
    {
        public AccountRepository(Context context) : base(context) { }

        public Account Get(int id)
        {
            return Context.Account.FirstOrDefault(p => p.AccountHolderId == id);
        }

        public void Update(Account account)
        {
            var entry = Context.Entry(account);
            entry.State = EntityState.Modified;

            SaveChanges();
        }
    }
}
