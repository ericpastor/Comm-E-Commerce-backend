using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;
using Comm.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Comm.WebAPI.src.Repositories
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {

        private DbSet<Category> _Categories;
        private DatabaseContext _database;

        public CategoryRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
            // _Categorys = database.Categorys;
            // _database = database;
        }

        public override async Task<IEnumerable<Category>> GetAllAsync(GetAllParams getAllParams)
        {
            return await _data.AsNoTracking().Include(c => c.Products).ThenInclude(p => p.Images).Include(c => c.CategoryImage).Skip(getAllParams.Offset).Take(getAllParams.Limit).ToArrayAsync(); ;
        }
        public async Task<Category?> FindByNameAsync(string name)
        {
            return await _data.AsNoTracking()
            .Include(c => c.CategoryImage)
            .Include(c => c.Products)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public override async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _data.Include(c => c.CategoryImage).Include(c => c.Products).ThenInclude(c => c.Images).FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<bool> UpdateOneAsync(Category updateObject)
        {
            var primaryKeyValue = _databaseContext.Entry(updateObject).Property("Id").CurrentValue;

            var existingEntity = await _data.FindAsync(primaryKeyValue);

            if (existingEntity is null)
            {
                return false;
            }

            var entry = _databaseContext.Entry(existingEntity);

            if (updateObject is Category category)
            {
                await UpdateCategorySpecificProperties(category, entry);
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

        private async Task UpdateCategorySpecificProperties(Category category, EntityEntry<Category> entry)
        {
            var originalCategoryImage = await _databaseContext.CategoryImages.SingleOrDefaultAsync(i => i.CategoryId == category.Id);
            var proposedCategoryImage = category.CategoryImage;

            if (originalCategoryImage != null)
            {
                if (proposedCategoryImage == null)
                {
                    _databaseContext.CategoryImages.Remove(originalCategoryImage);
                }
                else
                {
                    if (originalCategoryImage.CategoryImageUrl != proposedCategoryImage.CategoryImageUrl)
                    {
                        originalCategoryImage.CategoryImageUrl = proposedCategoryImage.CategoryImageUrl;
                    }

                }
            }
            else
            {
                if (proposedCategoryImage != null)
                {
                    _databaseContext.CategoryImages.Add(proposedCategoryImage);
                }
            }
            await _databaseContext.SaveChangesAsync();
        }

    }
}