using System.ComponentModel.DataAnnotations;

namespace Eshop.API.Models
{
    public class UploadFileInputDto
    {
        [Required]
        public IFormFile? File { get; set; }
    }

}
