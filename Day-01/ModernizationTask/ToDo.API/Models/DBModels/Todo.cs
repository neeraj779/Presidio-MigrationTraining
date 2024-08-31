using System.ComponentModel.DataAnnotations;

namespace ToDo.API.Models.DBModels
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TargetDate { get; set; }
        public bool Status { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
