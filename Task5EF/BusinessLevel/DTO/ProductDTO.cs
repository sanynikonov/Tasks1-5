using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLevel
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryDTO Category { get; set; }
        public ProviderDTO Provider { get; set; }
        public decimal Price { get; set; }
    }
}
