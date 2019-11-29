using System;
using System.Collections.Generic;
using System.Linq;
using DesafioVerity.Domain.Entity;
using DesafioVerity.Domain.Handlers;
using DesafioVerity.Domain.Interfaces.Entity;
using DesafioVerity.Repository.Infrastructure;

namespace DesafioVerity.Repository.Entity
{
    public class ExtractRepository : Common.Repository, IExtractRepository
    {
        public ExtractRepository(Context context) : base(context) { }

        public void Insert(Extract extract)
        {
            Context.Extract.Add(extract);

            SaveChanges();
        }

        public IList<Extract> List(ExtractRequest request)
        {
            var query = 
                Context.Extract.Where(p => p.AccountHolderId == request.AccountHolderId &&
                                           p.TransactionDate >= request.StartDate &&
                                           p.TransactionDate <= request.EndDate);
            
            return query.OrderByDescending(c=>c.TransactionDate).ToList();

        }
    }
}
