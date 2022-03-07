using System.ComponentModel.DataAnnotations;

namespace TesteTecnicoDigiStart.Domain
{
    public class UserDTO
    {
        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]        
        public string password { get; set; }
    }
}
