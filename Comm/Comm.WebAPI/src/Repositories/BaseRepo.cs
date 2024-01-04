using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;
using Comm.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Comm.WebAPI.src.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _data; // los ponemos protected por si tenemos que override
        protected readonly DatabaseContext _databaseContext;

        public BaseRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _data = _databaseContext.Set<T>();
        }

        public virtual async Task<T> CreateOneAsync(T createObject)
        {
            await _data.AddAsync(createObject);
            await _databaseContext.SaveChangesAsync();
            return createObject;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(GetAllParams getAllParams)
        {
            return await _data.AsNoTracking().Skip(getAllParams.Offset).Take(getAllParams.Limit).ToArrayAsync(); // el tracking es para obtimizar 
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _data.FindAsync(id);
        }

        public virtual async Task<bool> UpdateOneAsync(T updateObject)
        {
            var primaryKeyValue = _databaseContext.Entry(updateObject).Property("Id").CurrentValue;

            var existingEntity = await _data.FindAsync(primaryKeyValue);

            if (existingEntity is null)
            {
                return false;
            }

            var entry = _databaseContext.Entry(existingEntity);
            foreach (var property in entry.OriginalValues.Properties)
            {
                var originalValue = entry.OriginalValues[property];
                var proposedValue = entry.Property(property).CurrentValue;

                if (property.ClrType == typeof(string) && proposedValue is string proposedStringValue && string.IsNullOrEmpty(proposedStringValue) ||
                    property.ClrType == typeof(decimal) && proposedValue is decimal proposedDecimalValue && proposedDecimalValue <= 0)
                {
                    entry.Property(property).CurrentValue = originalValue;
                }
                else
                {
                    entry.Property(property).CurrentValue = proposedValue;
                }
            }


            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            var entity = await _data.FindAsync(id);
            if (entity is null)
            {
                return false; // Entity not found
            }

            _data.Remove(entity);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<T?> FindByEmailAsync(string email)
        {
            return await _data.FindAsync(email);
        }

        public async Task<T?> FindByNameAsync(string name)
        {
            return await _data.FindAsync(name);
        }
    }
}