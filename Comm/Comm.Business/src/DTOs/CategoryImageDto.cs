namespace Comm.Business.src.DTOs
{
    public class CategoryImageReadDto

    {
        // public Guid CategorytId { get; set; }
        public string CategoryImageUrl { get; set; }
        // public CategoryReadDto Product { get; set; }
    }

    public class CategoryImageCreateDto
    {
        public string CategoryImageUrl { get; set; }
    }

    public class CategoryImageUpdateDto
    {
        public string CategoryImageUrl { get; set; }
    }
}