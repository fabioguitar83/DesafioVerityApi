using DesafioVerity.Domain.Common;
using DesafioVerity.Domain.Entity;
using DesafioVerity.Domain.Handlers;
using DesafioVerity.Domain.Interfaces.Entity;
using DesafioVerity.Repository.Infrastructure;
using System.Linq;

namespace DesafioVerity.Repository.Entity
{
    public class AccountHolderRepository : Common.Repository, IAccountHolderRepository
    {
        public AccountHolderRepository(Context context) : base(context) { }

        public AccountHolder Login(LoginRequest login)
        {

            var accountHolder = Context.AccountHolder.FirstOrDefault(p => p.Agency == login.Agency &&
                                                                          p.AccountNumber == login.AccountNumber &&
                                                                          p.Password == EncryptPassword.Encrypt(login.Password));

            return accountHolder;

        }
    }
}
