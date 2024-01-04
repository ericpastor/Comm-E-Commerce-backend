namespace Comm.Core.src.Entities
{
    public class Product : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }

    }
}