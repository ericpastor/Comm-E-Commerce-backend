using Comm.Business.src.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.FormDtos
{
    public class ProductCreateFormDto
    {
        [FromForm(Name = "categoryId")]
        public Guid CategoryId { get; set; }

        [FromForm(Name = "title")]
        public string Title { get; set; }

        [FromForm(Name = "description")]
        public string Description { get; set; }

        [FromForm(Name = "price")]
        public decimal Price { get; set; }


        [FromForm(Name = "images")]
        public List<ImageCreateDto> Images { get; set; }
    }

    public class ProductUpdateFormDto
    {
        [FromForm(Name = "categoryId")]
        public Guid CategoryId { get; set; }

        [FromForm(Name = "title")]
        public string Title { get; set; }

        [FromForm(Name = "description")]
        public string Description { get; set; }

        [FromForm(Name = "price")]
        public decimal Price { get; set; }

        [FromForm(Name = "images")]
        public List<ImageCreateDto> Images { get; set; }
    }
}