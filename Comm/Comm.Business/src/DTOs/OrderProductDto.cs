namespace Comm.Business.src.DTOs
{
    public class OrderProductReadDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderProductCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderProductUpdateDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}