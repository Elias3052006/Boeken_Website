using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisinessLogicLayer.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Is_Available { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
    }
}

