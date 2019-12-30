using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        private IRepository<Category> categoryRepository;
        private IRepository<Product> productRepository;
        private IRepository<Provider> providerRepository;

        public UnitOfWork()
        {
            context = new DataContext();
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new Repository<Category>(context);
                }
                return categoryRepository;
            }
        }
        public IRepository<Provider> ProviderRepository
        {
            get
            {
                if (providerRepository == null)
                {
                    providerRepository = new Repository<Provider>(context);
                }
                return providerRepository;
            }
        }
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new Repository<Product>(context);
                }
                return productRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
