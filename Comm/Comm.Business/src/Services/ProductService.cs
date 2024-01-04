using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Business.src.Shared;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;

namespace Comm.Business.src.Services
{
    public class ProductService : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>, IProductService
    {
        private readonly ICategoryService _categoryService;

        public ProductService(IProductRepo repo, IMapper mapper, ICategoryService categoryService) : base(repo, mapper)
        {
            _categoryService = categoryService;
        }

        public override async Task<ProductReadDto> CreateOneAsync(ProductCreateDto createObject)
        {
            await _categoryService.GetByIdAsync(createObject.CategoryId); // Primero miro si esta la category

            var product = _mapper.Map<ProductCreateDto, Product>(createObject);
            var createdProduct = await _repo.CreateOneAsync(product);

            // var jsonSerializerOptions = new JsonSerializerOptions
            // {
            //     ReferenceHandler = ReferenceHandler.Preserve,
            // };

            return _mapper.Map<Product, ProductReadDto>(createdProduct);
        }

        public override async Task<IEnumerable<ProductReadDto>> GetAllAsync(GetAllParams getAllParams)
        {
            if (getAllParams.Offset < 0 || getAllParams.Limit < 0)
            {
                throw CustomException.InvalidValueException();
            }

            var products = await _repo.GetAllAsync(getAllParams);

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDto>>(products);
        }

        public override async Task<ProductReadDto> GetByIdAsync(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product is null)
            {
                throw CustomException.NotFoundException();
            }

            return _mapper.Map<Product, ProductReadDto>(product);
        }

        public override async Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto updateObject)
        {
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct is null)
            {
                throw CustomException.NotFoundException();
            }

            _mapper.Map(updateObject, existingProduct);

            return await _repo.UpdateOneAsync(existingProduct);
        }


        public override async Task<bool> DeleteOneAsync(Guid id)
        {
            var existingProduct = await _repo.GetByIdAsync(id);
            if (existingProduct is null)
            {
                throw CustomException.NotFoundException();
            }

            return await _repo.DeleteOneAsync(id);
        }



    }
}
