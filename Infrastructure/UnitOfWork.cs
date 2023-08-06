using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        public IProductRepository Products => _products ?? (_products = new ProductRepository(dbContext));

        private IProductRepository _products;

        private bool disposed = false;

        public UnitOfWork()
        {
            dbContext = new OnlineShopContext();
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(false);
            GC.SuppressFinalize(this);
        }
        protected virtual async ValueTask DisposeAsyncCore()
        {
            await dbContext.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        { 
            if(disposed) return;
            if(disposing)
            {
                dbContext.Dispose();
            }
            disposed = true;
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
    }
}
