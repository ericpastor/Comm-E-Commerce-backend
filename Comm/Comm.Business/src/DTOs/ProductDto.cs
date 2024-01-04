using Comm.Core.src.Entities;

namespace Comm.Business.src.DTOs
{

    public class ProductReadDto : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public IEnumerable<ImageReadDto> Images { get; set; }
    }
    public class ProductCreateDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ImageCreateDto> Images { get; set; }
    }
    public class ProductUpdateDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ImageCreateDto> Images { get; set; }
    }

}