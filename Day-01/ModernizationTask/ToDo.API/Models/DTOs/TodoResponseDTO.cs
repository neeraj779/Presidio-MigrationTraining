namespace ToDo.API.Models.DTOs
{
    public class TodoResponseDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int TodoId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TargetDate { get; set; }
        public bool Status { get; set; }
    }
}
