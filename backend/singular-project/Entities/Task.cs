using System.Security.Principal;
using System.Text.Json.Serialization;

namespace singular_project.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
        [JsonIgnore]
        public CSV? CSV { get; set; }
        public int? CSVId { get; set; }
        
    }
}
