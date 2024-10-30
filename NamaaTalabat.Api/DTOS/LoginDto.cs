using System.ComponentModel.DataAnnotations;

namespace NamaaTalabat.Api.DTOS
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //[ DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
