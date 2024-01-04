using Comm.Business.src.DTOs;
using Comm.Core.src.Entities;

namespace Comm.Business.src.Interfaces
{
    public interface IProductService : IBaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
    }
}