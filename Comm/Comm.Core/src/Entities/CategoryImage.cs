namespace Comm.Core.src.Entities
{
    public class CategoryImage : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public string CategoryImageUrl { get; set; }
    }
}