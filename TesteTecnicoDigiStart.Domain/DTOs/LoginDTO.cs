using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoDigiStart.Domain
{
    public class LoginDTO
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
