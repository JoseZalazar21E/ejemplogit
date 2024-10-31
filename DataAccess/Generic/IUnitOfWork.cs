using Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        ApiDbContext Context { get; }
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public ApiDbContext Context { get; }
        public UnitOfWork(ApiDbContext context)
        {
            Context = context;
        }
        void IUnitOfWork.Commit()
        {
            Context.SaveChanges();
        }
        void IDisposable.Dispose()
        {
            Context.Dispose();
        }
    }
}
