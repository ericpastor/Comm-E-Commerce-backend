using Comm.Core.src.Entities;
using Comm.Core.src.Parameters;

namespace Comm.Business.src.Interfaces
{
    public interface IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
        where T : BaseEntity
    {
        Task<IEnumerable<TReadDto>> GetAllAsync(GetAllParams getAllParams);
        Task<TReadDto> GetByIdAsync(Guid id);
        Task<bool> UpdateOneAsync(Guid id, TUpdateDto upDateObject);
        Task<bool> DeleteOneAsync(Guid id);
        Task<TReadDto> CreateOneAsync(TCreateDto createObject); // este lo he cambiado ya que creaba conflicto
    }
}