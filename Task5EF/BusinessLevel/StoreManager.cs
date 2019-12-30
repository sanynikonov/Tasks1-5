using AutoMapper;
using DataLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLevel
{
    public class StoreManager : IDisposable
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public StoreManager()
        {
            unitOfWork = new UnitOfWork();
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Provider, ProviderDTO>();
                cfg.CreateMap<ProviderDTO, Provider>();
            });
            mapper = new Mapper(configuration);
        }

        private IEnumerable<TOutput> ConvertEntitiesToDTOs<TInput, TOutput>(IEnumerable<TInput> input)
        {
            List<TOutput> productsDTO = new List<TOutput>();
            foreach (var product in input)
                productsDTO.Add(mapper.Map<TOutput>(product));
            return productsDTO;
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(string categoryName)
        {
            var products = unitOfWork.ProductRepository.GetAll().ToList();
            var productsByCategory = products.Where(x => x.Category.Name == categoryName);
            return ConvertEntitiesToDTOs<Product, ProductDTO>(productsByCategory);
        }

        public IEnumerable<ProviderDTO> GetProvidersByCategory(string categoryName)
        {
            var providers = unitOfWork.ProductRepository.GetAll()
                .Where(product => product.Category.Name == categoryName)
                .Select(x => x.Provider)
                .Distinct();
            return ConvertEntitiesToDTOs<Provider, ProviderDTO>(providers);
        }

        public IEnumerable<ProductDTO> GetProductsByProvider(string providerName)
        {
            var products = unitOfWork.ProductRepository.GetAll().Where(x => x.Provider.Name == providerName);
            return ConvertEntitiesToDTOs<Product, ProductDTO>(products);
        }

        public IEnumerable<ProductDTO> GetProductsWithMaxPrice()
        {
            var allProducts = unitOfWork.ProductRepository.GetAll();
            decimal maxPrice = allProducts.Max(x => x.Price);
            var products = allProducts.Where(x => x.Price == maxPrice);

            return ConvertEntitiesToDTOs<Product, ProductDTO>(products);
        }

        public IEnumerable<ProductDTO> GetProductsByPrice(decimal min, decimal max)
        {
            var allProducts = unitOfWork.ProductRepository.GetAll();
            var products = allProducts.Where(x => x.Price >= min && x.Price <= max);

            return ConvertEntitiesToDTOs<Product, ProductDTO>(products);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
