namespace Comm.Business.src.DTOs
{

    public class AvatarReadDto

    {
        // public Guid UsertId { get; set; }
        public string AvatarUrl { get; set; }
        // public UserReadDto Product { get; set; }
    }

    public class AvatarCreateDto
    {
        public string AvatarUrl { get; set; }
    }

    public class AvatarUpdateDto
    {
        public string AvatarUrl { get; set; }
    }
}