using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public virtual Provider Provider { get; set; }
        public decimal Price { get; set; }
    }
}
