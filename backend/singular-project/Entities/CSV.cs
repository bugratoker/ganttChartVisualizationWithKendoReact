namespace singular_project.Entities
{
    public class CSV
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}
