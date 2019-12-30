using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DataContext() : base("DBConnection")
        {
        }

        static DataContext()
        {
            Database.SetInitializer(new DBInitializer());
        }
    }

    public class DBInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var food = new Category { Name = "Food" };
            var chemistry = new Category { Name = "Household chemistry" };
            var lifestyle = new Category { Name = "Lifestyle" };
            var music = new Category { Name = "Music" };
            var education = new Category { Name = "Education" };

            var roshen = new Provider { Name = "Roshen" };
            var huawei = new Provider { Name = "Huawei" };
            var lingvo = new Provider { Name = "ABBYY Lingvo" };
            var png = new Provider { Name = "P&G" };
            var yamaha = new Provider { Name = "Yamaha" };

            Product[] products =
            {
                new Product {Name = "Cookies", Provider = roshen, Category = food, Price = 20m},
                new Product {Name = "Chocolate", Provider = roshen, Category = food, Price = 30m},
                new Product {Name = "Marshmallow", Provider = roshen, Category = food, Price = 40m},
                new Product {Name = "Xiaomi", Provider = huawei, Category = lifestyle, Price = 43m},
                new Product {Name = "Smarthone", Provider = huawei, Category = lifestyle, Price = 42m},
                new Product {Name = "JBL dynamic", Provider = huawei, Category = music, Price = 60m},
                new Product {Name = "Dictionary", Provider = lingvo, Category = education, Price = 40m},
                new Product {Name = "Vocabulary", Provider = lingvo, Category = education, Price = 32m},
                new Product {Name = "Tide", Provider = png, Category = chemistry, Price = 40m},
                new Product {Name = "Mr Propper", Provider = png, Category = chemistry, Price = 31m},
                new Product {Name = "Acoustic guitar", Provider = yamaha, Category = music, Price = 41m},
                new Product {Name = "Stratocaster", Provider = yamaha, Category = music, Price = 30m},
            };

            context.Products.AddRange(products);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
