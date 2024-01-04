using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;
using Comm.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Comm.WebAPI.src.Repositories
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {

        private DbSet<Product> _Products;
        private DatabaseContext _database;

        public ProductRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
            // _Products = database.Products;
            // _database = database;
        }

        public async Task<Product?> FindByTitleAsync(string title)
        {
            return await _data.AsNoTracking().FirstOrDefaultAsync(p => p.Title == title);
        }

        public override async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _data.Include(product => product.Images).Include(product => product.Category).FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync(GetAllParams getAllParams)
        {
            return await _data.AsNoTracking().Include(product => product.Images).Include(product => product.Category).Skip(getAllParams.Offset).Take(getAllParams.Limit).ToArrayAsync(); // el tracking es para obtimizar 
        }

        public override async Task<bool> UpdateOneAsync(Product updateObject)
        {
            var primaryKeyValue = _databaseContext.Entry(updateObject).Property("Id").CurrentValue;

            var existingEntity = await _data.FindAsync(primaryKeyValue);

            if (existingEntity is null)
            {
                return false;
            }

            var entry = _databaseContext.Entry(existingEntity);

            if (updateObject is Product product)
            {
                await UpdateUserSpecificProperties(product, entry);
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

        private async Task UpdateUserSpecificProperties(Product product, EntityEntry<Product> entry)
        {
            await _databaseContext.Database.ExecuteSqlRawAsync("ALTER TABLE images DROP CONSTRAINT fk_images_products_product_id;");
            await _databaseContext.Database.ExecuteSqlRawAsync("ALTER TABLE images ADD CONSTRAINT fk_images_products_product_id FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE;");

            var originalImages = await _databaseContext.Images.Where(a => a.ProductId == product.Id).ToListAsync();
            var proposedImages = product.Images;

            foreach (var originalImage in originalImages)
            {
                var proposedImage = proposedImages.FirstOrDefault(a => a.Id == originalImage.Id);

                if (proposedImage == null)
                {
                    _databaseContext.Images.Remove(originalImage);
                }
            }

            var newImages = proposedImages.Where(pa => originalImages.All(oa => oa.Id != pa.Id));
            _databaseContext.Images.AddRange(newImages);

            await _databaseContext.SaveChangesAsync();
        }

    }
}