namespace Comm.Core.src.Entities
{
    public class Image : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
        public Product Product { get; set; }
    }
}