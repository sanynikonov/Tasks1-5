using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5ADO
{
    public class ProductDataAccesor
    {
        private string connectionString;
        private CategoryDataAccesor categoryDataAccesor;
        private ProviderDataAccesor providerDataAccesor;

        public ProductDataAccesor(string connectionString)
        {
            this.connectionString = connectionString;
            categoryDataAccesor = new CategoryDataAccesor(connectionString);
            providerDataAccesor = new ProviderDataAccesor(connectionString);
        }

        public Product GetProduct(int id)
        {
            string query = "SELECT * FROM Product WHERE Id = @id";
            Product product = null;
            int categoryId = 0;
            int providerId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product = new Product();
                            product.Id = Convert.ToInt32(reader["Id"]);
                            product.Name = (string)reader["Name"];
                            categoryId = Convert.ToInt32(reader["CategoryId"]);
                            providerId = Convert.ToInt32(reader["ProviderId"]);
                        }
                    }
                }
            }

            if (product != null)
            {
                Category category = categoryDataAccesor.GetCategory(categoryId);
                Provider provider = providerDataAccesor.GetProvider(providerId);
                product.Category = category;
                product.Provider = provider;
                return product;
            }
            else return null;
        }

        public IEnumerable<Product> GetProducts()
        {
            string query = "SELECT * FROM Products";
            List<Product> products = new List<Product>();
            List<Category> categories = categoryDataAccesor.GetCategories().ToList();
            List<Provider> providers = providerDataAccesor.GetProviders().ToList();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int categoryId;
                        int providerId;
                        Product product;
                        while (reader.Read())
                        {
                            providerId = Convert.ToInt32(reader["ProviderId"]);
                            categoryId = Convert.ToInt32(reader["CategoryId"]);
                            product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"],
                                Category = categories.Find(x => x.Id == categoryId),
                                Provider = providers.Find(x => x.Id == providerId)
                            };
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public void AddProduct(Product product)
        {
            string query = "INSERT INTO Products(Name, CategoryId, ProviderId) VALUES(@name, @categoryId, @providerId);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@categoryId", product.Category.Id);
                    command.Parameters.AddWithValue("@providerId", product.Provider.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            string query = "UPDATE Products SET Name = @name, CategoryId = @categoryId, ProviderId = @providerId WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@categoryId", product.Category.Id);
                    command.Parameters.AddWithValue("@providerId", product.Provider.Id);
                    command.Parameters.AddWithValue("@id", product.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveProduct(Product product)
        {
            string query = "DELETE Products WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", product.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
