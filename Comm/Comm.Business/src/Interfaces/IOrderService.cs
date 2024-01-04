using Comm.Business.src.DTOs;
using Comm.Core.src.Entities;

namespace Comm.Business.src.Interfaces
{
    public interface IOrderService : IBaseService<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
    {

    }
}