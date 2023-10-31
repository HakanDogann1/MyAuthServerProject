using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServerProject.Core.DTOs
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
    }
}
