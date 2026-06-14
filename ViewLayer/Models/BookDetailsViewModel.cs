namespace ViewLayer.Models
{
    public class BookDetailViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public int Stock { get; set; }
        public bool IsAvailable { get; set; }
    }
}
