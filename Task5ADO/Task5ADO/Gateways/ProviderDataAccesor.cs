using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5ADO
{
    public class ProviderDataAccesor
    {
        private string connectionString;

        public ProviderDataAccesor(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Provider GetProvider(int id)
        {
            string query = "SELECT * FROM Providers WHERE Id = @id";
            Provider provider = null;
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
                            provider = new Provider
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"]
                            };
                        }
                    }
                }
            }
            return provider;
        }

        public IEnumerable<Provider> GetProviders()
        {
            string query = "SELECT * FROM Providers";
            List<Provider> providers = new List<Provider>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Provider provider;
                        while (reader.Read())
                        {
                            provider = new Provider
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"]
                            };
                            providers.Add(provider);
                        }
                    }
                }
            }
            return providers;
        }

        public void AddProvider(Provider provider)
        {
            string query = "INSERT INTO Providers(Name) VALUES(@name);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", provider.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProvider(Provider provider)
        {
            string query = "UPDATE Providers SET Name = @name WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", provider.Name);
                    command.Parameters.AddWithValue("@id", provider.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveProvider(Provider provider)
        {
            string query = "DELETE Providers WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", provider.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
