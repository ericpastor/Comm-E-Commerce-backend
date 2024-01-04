using Comm.Core.src.Entities;

namespace Comm.Business.src.DTOs
{
    public class OrderReadDto : BaseEntity
    {
        public Status Status { get; set; }
        public Guid UserId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }

    public class OrderCreateDto
    {
        public Guid UserId { get; set; }
        public List<OrderProductCreateDto> OrderProducts { get; set; }
    }


    public class OrderUpdateDto
    {
        public Status Status { get; set; }
        public Guid UserId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}