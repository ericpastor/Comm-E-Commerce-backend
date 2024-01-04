using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;
using Comm.Core.src.Parameters;
using Comm.WebAPI.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Comm.WebAPI.src.Repositories
{

    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private DbSet<Product> _products;
        public OrderRepo(DatabaseContext database) : base(database)
        {
            _products = database.Products;
        }

        public override async Task<IEnumerable<Order>> GetAllAsync(GetAllParams options)
        {
            return await _data.AsNoTracking().Include(order => order.OrderProducts).ThenInclude(order => order.Product)
                .Skip(options.Offset).Take(options.Limit).ToListAsync();
        }
        // .ThenInclude(order => order.Category).Include(order => order.User).ThenInclude(order => order.Addresses)

        public override async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _data.AsNoTracking().Include(order => order.OrderProducts).ThenInclude(order => order.Product).ThenInclude(order => order.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<Order> CreateOneAsync(Order createObject)
        {
            using (var transaction = await _databaseContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (OrderProduct op in createObject.OrderProducts)
                    {
                        Product product = await _products.FirstOrDefaultAsync(p => p.Id == op.ProductId)!;
                        _products.Update(product);
                        await _databaseContext.SaveChangesAsync();
                    }
                    _data.Add(createObject);
                    await _databaseContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return await GetByIdAsync(createObject.Id);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}