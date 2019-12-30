using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5ADO
{
    public class CategoryDataAccesor
    {
        private string connectionString;

        public CategoryDataAccesor(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Category GetCategory(int id)
        {
            string query = "SELECT * FROM Categories WHERE Id = @id";
            Category category = null;
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
                            category = new Category
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"]
                            };
                        }
                    }
                }
            }
            return category;
        }

        public void AddCategory(Category category)
        {
            string query = "INSERT INTO Categories(Name) VALUES(@name);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCategory(Category category)
        {
            string query = "UPDATE Categories SET Name = @name WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@id", category.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveCategory(Category category)
        {
            string query = "DELETE Categories WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", category.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            string query = "SELECT * FROM Categories";
            List<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Category category;
                        while (reader.Read())
                        {
                            category = new Category
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"]
                            };
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }
    }
}
