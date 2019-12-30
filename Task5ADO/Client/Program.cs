using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Programming\C#\Task5ADO\Task5ADO\MyDataBase.mdf;Integrated Security=True";
            var str = System.Configuration.ConfigurationManager.ConnectionStrings


            BusinessHandler business = new BusinessHandler(connectionString);
            StringBuilder sb = new StringBuilder();


            Console.WriteLine("Как же я люблю рошен! Вот они слева направо: ");
            string prodsByProvider = business.GetProductsByProvider("Roshen").Select(x => x.Name).Aggregate((x, y) => x + ", " + y);
            Console.WriteLine(prodsByProvider + "\n");

            Console.WriteLine("Как же я люблю спорт-товары! Вот они слева направо: ");
            string prodsByCategory = business.GetProductsByCategory("Sports").Select(x => x.Name).Aggregate((x, y) => x + ", " + y);
            Console.WriteLine(prodsByCategory + "\n");

            Console.WriteLine("Как же я люблю производителей музыкальных товаров. Вот они слева направо: ");
            string provsByCategory = business.GetProvidersByCategory("Sports").Select(x => x.Name).Aggregate((x, y) => x + ", " + y);
            Console.WriteLine(provsByCategory + "\n");

            Console.WriteLine("Как же я люблю производителей-монополистов с наибольшим количеством категорий. Вот они слева направо: ");
            string provsWithMaxCategories = business.GetProvidersWithMaxProductCategories().Select(x => x.Name).Aggregate((x, y) => x + ", " + y);
            Console.WriteLine(provsWithMaxCategories + "\n");

            Console.ReadLine();


        }

    }
}
