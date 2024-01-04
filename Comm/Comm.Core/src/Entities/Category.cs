namespace Comm.Core.src.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public CategoryImage CategoryImage { get; set; }
        public IEnumerable<Product> Products { get; set; }

    }
}