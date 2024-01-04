using Comm.Business.src.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    public class CategoryCreateFormDto
    {
        [FromForm(Name = "name")]
        public string Name { get; set; }
        [FromForm(Name = "categoryImage")]
        public CategoryImageCreateDto CategoryImage { get; set; }
    }

    public class CategoryUpdateFormDto
    {
        [FromForm(Name = "name")]
        public string Name { get; set; }
        [FromForm(Name = "categoryImage")]
        public CategoryImageCreateDto CategoryImage { get; set; }
    }
}