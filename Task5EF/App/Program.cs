using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLevel;
using BusinessLevel;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StoreManager store = new StoreManager())
            {
                var products = store.GetProductsByCategory("Food").ToList();
                Console.WriteLine("Roshen products");
                products.ForEach(x => Console.WriteLine(x.Name));

                Console.ReadLine();
            }
        }
    }
}
