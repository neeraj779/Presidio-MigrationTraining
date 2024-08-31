using System.ComponentModel.DataAnnotations;

namespace ToDo.API.Models.DBModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public byte[] HashedPassword { get; set; } = new byte[0];

        [Required]
        public byte[] PasswordHashKey { get; set; } = new byte[0];

        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}
