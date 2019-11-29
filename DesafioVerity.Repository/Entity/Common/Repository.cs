using DesafioVerity.Domain.Interfaces.Entity;
using DesafioVerity.Repository.Infrastructure;
using System;

namespace DesafioVerity.Repository.Entity.Common
{
    public abstract class Repository : IRepository
    {
        private readonly Context _context;

        protected Context Context
        {
            get { return _context; }
        }

        public Repository(Context context)
        {
            _context = context;          
        }
      
        public void SaveChanges()
        {
            if (Context.ChangeTracker.HasChanges())
                Context.SaveChanges();
        }
    }
}
