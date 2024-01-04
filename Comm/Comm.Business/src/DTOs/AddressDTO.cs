using Comm.Core.src.Entities;

namespace Comm.Business.src.DTO
{
    public class AddressReadDto : BaseEntity
    {
        public int HouseNumber { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
    }

    public class AddressCreateDto
    {
        public int HouseNumber { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
    }

    public class AddressUpdateDto
    {
        public int HouseNumber { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
    }
}