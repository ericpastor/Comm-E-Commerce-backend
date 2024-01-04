using Comm.Core.src.Entities;

namespace Comm.Business.src.DTOs
{
    public class CategoryReadDto : BaseEntity
    {
        public string Name { get; set; }
        public CategoryImageReadDto CategoryImage { get; set; }
        public IEnumerable<ProductReadDto> Products { get; set; }
    }

    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public CategoryImageCreateDto CategoryImage { get; set; }
    }

    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public CategoryImageCreateDto CategoryImage { get; set; }
    }
}