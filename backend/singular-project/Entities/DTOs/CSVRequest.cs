namespace singular_project.Entities.DTOs
{
    public class CSVRequest
    {
        public string CSVName { get; set; }
        public List<TaskRequest> Tasks { get; set; }
    }
}
