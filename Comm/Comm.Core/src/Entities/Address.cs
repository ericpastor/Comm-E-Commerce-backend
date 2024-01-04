namespace Comm.Core.src.Entities
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        public int HouseNumber { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public User User { get; set; }
    }
}