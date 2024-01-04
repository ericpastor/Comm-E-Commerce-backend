using Comm.Core.src.Entities;

namespace Comm.Business.src.DTOs
{

    public class ImageReadDto : BaseEntity

    {
        // public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
        // public ProductReadDto Product { get; set; }
    }

    public class ImageCreateDto
    {
        public string ImageUrl { get; set; }
    }

    public class ImageUpdateDto
    {
        public string ImageUrl { get; set; }
    }

}