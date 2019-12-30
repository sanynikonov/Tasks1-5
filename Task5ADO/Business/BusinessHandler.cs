using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5ADO;

namespace Business
{
    public class BusinessHandler
    {
        private ProviderDataAccesor providerDataAccesor;
        private ProductDataAccesor productDataAccesor;
        private CategoryDataAccesor categoryDataAccesor;

        public BusinessHandler(string connectionString)
        {
            providerDataAccesor = new ProviderDataAccesor(connectionString);
            productDataAccesor = new ProductDataAccesor(connectionString);
            categoryDataAccesor = new CategoryDataAccesor(connectionString);
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            var products = productDataAccesor.GetProducts();
            return products.Where(x => x.Category.Name == categoryName);
        }

        public IEnumerable<Provider> GetProvidersByCategory(string categoryName)
        {
            var products = GetProductsByCategory(categoryName);
            return products.Select(x => x.Provider).Distinct();
        }

        public IEnumerable<Product> GetProductsByProvider(string providerName)
        {
            var products = productDataAccesor.GetProducts();
            return products.Where(x => x.Provider.Name == providerName);
        }

        public IEnumerable<Provider> GetProvidersWithMaxProductCategories()
        {
            var products = productDataAccesor.GetProducts();
            var groups = products.GroupBy(x => x.Provider);

            var providersWithUniqueCategoriesCounts = groups.Select(group => new {
                    Provider = group.Key,
                    CategoriesCount = group.GroupBy(product => product.Category).Select(y => y.First()).Count()
                });
            int maxCategoriesCount = providersWithUniqueCategoriesCounts.Max(x => x.CategoriesCount);

            return providersWithUniqueCategoriesCounts.Where(x => x.CategoriesCount == maxCategoriesCount).Select(y => y.Provider);
        }
    }
}
