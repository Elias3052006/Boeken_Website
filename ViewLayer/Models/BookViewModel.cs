using BisinessLogicLayer.DTOs;
using System.Collections.Generic;

namespace ViewLayer.Models
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    }

}
