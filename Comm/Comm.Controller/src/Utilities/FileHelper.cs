using Microsoft.AspNetCore.Http;

namespace Comm.Controller.src.Utilities
{
    public class FileHelper
    {
        public static async Task<byte[]> ConvertIFormFileToByteArrayAsync(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}