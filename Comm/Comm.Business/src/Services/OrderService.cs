using AutoMapper;
using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Business.src.Shared;
using Comm.Core.src.Entities;
using Comm.Core.src.Interfaces;


namespace Comm.Business.src.Services
{
    public class OrderService : BaseService<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>, IOrderService
    {
        private IUserRepo _userRepo;
        private IProductRepo _productRepo;

        public OrderService(IOrderRepo repo, IUserRepo userRepo, IProductRepo productRepo, IMapper mapper) : base(repo, mapper)
        {
            _userRepo = userRepo;
            _productRepo = productRepo;
        }

        public async Task<OrderReadDto> CreateOrderAsync(Guid id, OrderCreateDto createObject)
        {
            User? user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw CustomException.NotFoundException();
            }
            foreach (OrderProductCreateDto dto in createObject.OrderProducts)
            {
                Product? product = await _productRepo.GetByIdAsync(dto.ProductId);
                if (product == null)
                {
                    throw CustomException.NotFoundException("Product not found");
                }
            }
            Order order = _mapper.Map<OrderCreateDto, Order>(createObject);
            order.UserId = id;


            return _mapper.Map<Order, OrderReadDto>(await _repo.CreateOneAsync(order));
        }
    }
}