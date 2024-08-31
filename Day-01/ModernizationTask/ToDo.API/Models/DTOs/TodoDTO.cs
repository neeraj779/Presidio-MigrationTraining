namespace ToDo.API.Models.DTOs
{
    public class TodoDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TargetDate { get; set; }
        public bool Status { get; set; }
    }
}
