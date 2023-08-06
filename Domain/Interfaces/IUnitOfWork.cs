using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IProductRepository Products { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
