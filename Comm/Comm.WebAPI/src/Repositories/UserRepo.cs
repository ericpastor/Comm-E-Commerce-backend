using Microsoft.EntityFrameworkCore;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Entities;
using Comm.WebAPI.src.Database;
using Comm.WebAPI.src.Repositories;
using Comm.Core.src.Parameters;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Comm.WebAPI.src.Repository
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        private DbSet<User> _users;
        private DatabaseContext _database;

        public UserRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
            // _users = database.Users;
            // _database = database;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _data.AsNoTracking().Include(user => user.Addresses).Include(user => user.Avatar).FirstOrDefaultAsync(u => u.Email == email);
        }

        public override async Task<User?> GetByIdAsync(Guid id)
        {
            return await _data.Include(user => user.Addresses).Include(user => user.Avatar).FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<IEnumerable<User>> GetAllAsync(GetAllParams getAllParams)
        {
            return await _data.AsNoTracking().Include(user => user.Addresses).Include(user => user.Avatar).Skip(getAllParams.Offset).Take(getAllParams.Limit).ToArrayAsync(); // el tracking es para obtimizar 
        }

        public override async Task<bool> UpdateOneAsync(User updateObject)
        {
            var primaryKeyValue = _databaseContext.Entry(updateObject).Property("Id").CurrentValue;

            var existingEntity = await _data.FindAsync(primaryKeyValue);

            if (existingEntity is null)
            {
                return false;
            }

            var entry = _databaseContext.Entry(existingEntity);

            if (updateObject is User user)
            {
                await UpdateUserSpecificProperties(user, entry);
            }
            else
            {
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
            }

            await _databaseContext.SaveChangesAsync();

            return true;
        }

        private async Task UpdateUserSpecificProperties(User user, EntityEntry<User> entry)
        {
            await _databaseContext.Database.ExecuteSqlRawAsync("ALTER TABLE adresses DROP CONSTRAINT fk_adresses_users_user_id;");
            await _databaseContext.Database.ExecuteSqlRawAsync("ALTER TABLE adresses ADD CONSTRAINT fk_adresses_users_user_id FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE;");

            var originalAddresses = await _databaseContext.Adresses.Where(a => a.UserId == user.Id).ToListAsync();
            var proposedAddresses = user.Addresses;

            foreach (var originalAddress in originalAddresses)
            {
                var proposedAddress = proposedAddresses.FirstOrDefault(a => a.Id == originalAddress.Id);

                if (proposedAddress == null)
                {
                    _databaseContext.Adresses.Remove(originalAddress);
                }
            }

            var newAddresses = proposedAddresses.Where(pa => originalAddresses.All(oa => oa.Id != pa.Id));
            _databaseContext.Adresses.AddRange(newAddresses);

            var originalAvatar = await _databaseContext.Avatars.SingleOrDefaultAsync(a => a.UserId == user.Id);
            var proposedAvatar = user.Avatar;

            if (originalAvatar != null)
            {
                if (proposedAvatar == null)
                {
                    _databaseContext.Avatars.Remove(originalAvatar);
                }
                else
                {
                    if (originalAvatar.AvatarUrl != proposedAvatar.AvatarUrl)
                    {
                        originalAvatar.AvatarUrl = proposedAvatar.AvatarUrl;
                    }

                }
            }
            else
            {
                if (proposedAvatar != null)
                {
                    _databaseContext.Avatars.Add(proposedAvatar);
                }
            }

            await _databaseContext.SaveChangesAsync();
        }

    }
}