using DesafioVerity.Domain.Entity;

namespace DesafioVerity.Domain.Interfaces.Entity
{
    public interface IAccountRepository
    {
        void Update(Account account);
        Account Get(int id);
    }
}
