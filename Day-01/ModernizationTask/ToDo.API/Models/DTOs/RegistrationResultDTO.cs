using System.ComponentModel.DataAnnotations;

namespace ToDo.API.Models.DTOs
{
    public class RegistrationResultDTO
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
