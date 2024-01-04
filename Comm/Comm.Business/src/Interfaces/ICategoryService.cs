using Comm.Business.src.DTOs;
using Comm.Core.src.Entities;

namespace Comm.Business.src.Interfaces
{
    public interface ICategoryService : IBaseService<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>
    {
        Task<CategoryReadDto> GetCategoryWithProductsAsync(string categoryName);
    }
}