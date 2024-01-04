using AutoMapper;
using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Business.src.Shared;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;

namespace Comm.Business.src.Services
{
    public class CategoryService : BaseService<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>, ICategoryService
    {
        public CategoryService(ICategoryRepo repo, IMapper mapper) : base(repo, mapper)
        { }
        public override async Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createObject)
        {

            var Category = _mapper.Map<CategoryCreateDto, Category>(createObject);

            var createdCategory = await _repo.CreateOneAsync(Category);

            return _mapper.Map<Category, CategoryReadDto>(createdCategory);
        }

        public override async Task<IEnumerable<CategoryReadDto>> GetAllAsync(GetAllParams getAllParams)
        {
            if (getAllParams.Offset < 0 || getAllParams.Limit < 0)
            {
                throw CustomException.InvalidValueException();
            }

            var Categorys = await _repo.GetAllAsync(getAllParams);

            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryReadDto>>(Categorys);
        }

        public override async Task<CategoryReadDto> GetByIdAsync(Guid id)
        {
            var category = await _repo.GetByIdAsync(id);

            if (category is null)
            {
                throw CustomException.CategoryNotFoundException();
            }

            return _mapper.Map<Category, CategoryReadDto>(category);
        }

        public override async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateObject)
        {
            var existingCategory = await _repo.GetByIdAsync(id);
            if (existingCategory is null)
            {
                throw CustomException.NotFoundException();
            }

            _mapper.Map(updateObject, existingCategory);

            return await _repo.UpdateOneAsync(existingCategory);
        }


        public override async Task<bool> DeleteOneAsync(Guid id)
        {
            var existingCategory = await _repo.GetByIdAsync(id);
            if (existingCategory is null)
            {
                throw CustomException.NotFoundException();
            }

            return await _repo.DeleteOneAsync(id);
        }

        public async Task<CategoryReadDto> GetCategoryWithProductsAsync(string categoryName)
        {
            var category = await _repo.FindByNameAsync(categoryName);

            if (category == null)
            {
                throw CustomException.CategoryNotFoundException();
            }

            return _mapper.Map<Category, CategoryReadDto>(category);
        }

    }
}