using DesafioVerity.Domain.Entity;
using DesafioVerity.Domain.Handlers;

namespace DesafioVerity.Domain.Interfaces.Entity
{
    public interface IAccountHolderRepository
    {
        AccountHolder Login(LoginRequest accountHolder);
    }
}
