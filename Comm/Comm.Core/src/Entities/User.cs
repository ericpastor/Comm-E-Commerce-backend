using System.Text.Json.Serialization;

namespace Comm.Core.src.Entities

{
    public class User : BaseEntity
    {
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public Avatar Avatar { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }


    }
    [JsonConverter(typeof(JsonStringEnumConverter))] // para convertirlo en un string -- sino pondria 0 o 1 en vez de "admin" o "customer"
    public enum Role
    {
        Admin,
        Customer
    }
}