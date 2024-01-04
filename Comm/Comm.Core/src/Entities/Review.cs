namespace Comm.Core.src.Entities
{
    public class Review : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
        public Product Product { get; set; }
        public User Customer { get; set; }
    }
}