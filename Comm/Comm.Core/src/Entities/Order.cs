using System.Text.Json.Serialization;

namespace Comm.Core.src.Entities
{
    public class Order : BaseEntity
    {
        public Status Status { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Unpaid,
        Paid,
        Pending,
        Dispatched,
        Delivered,
    }
}