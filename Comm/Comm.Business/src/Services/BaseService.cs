using AutoMapper;
using Comm.Business.src.Interfaces;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;

namespace Comm.Business.src.Services
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto> : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
        where T : BaseEntity //este se pone porque sino habria conflicto con el BaseEntity
    {
        protected readonly IBaseRepo<T> _repo; //si le ponemos protected los podemos usar en las clases deribadas
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepo<T> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // se hacen virtual para poder override
        public virtual async Task<TReadDto> CreateOneAsync(TCreateDto createObject)
        {
            return _mapper.Map<T, TReadDto>(await _repo.CreateOneAsync(_mapper.Map<TCreateDto, T>(createObject)));
        }

        public virtual async Task<IEnumerable<TReadDto>> GetAllAsync(GetAllParams getAllParams)
        {
            IEnumerable<T> entities = await _repo.GetAllAsync(getAllParams);
            return _mapper.Map<IEnumerable<T>, IEnumerable<TReadDto>>(entities);
        }

        public virtual async Task<TReadDto?> GetByIdAsync(Guid id)
        {
            T entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return default;
            }

            return _mapper.Map<T, TReadDto>(entity);
        }

        public virtual async Task<bool> UpdateOneAsync(Guid id, TUpdateDto updateObject)
        {
            T existingEntity = await _repo.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return false;
            }

            T updatedEntity = _mapper.Map(updateObject, existingEntity);
            return await _repo.UpdateOneAsync(updatedEntity);
        }

        public virtual async Task<bool> DeleteOneAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await _repo.DeleteOneAsync(id);

            return true;
        }


    }
}