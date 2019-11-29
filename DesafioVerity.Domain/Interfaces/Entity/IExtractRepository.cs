using DesafioVerity.Domain.Entity;
using DesafioVerity.Domain.Handlers;
using System.Collections.Generic;

namespace DesafioVerity.Domain.Interfaces.Entity
{
    public interface IExtractRepository
    {
        void Insert(Extract extract);
        IList<Extract> List(ExtractRequest request);
    }
}
