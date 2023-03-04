using System.ComponentModel.DataAnnotations;

namespace api_app.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Client { get; set; }
        public Job Job { get; set; }
        public int JobId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Construction_manager { get; set; }
        public string Supervisor { get; set; }
        public string Reference { get; set; }
        public User Leader { get; set; }
        public int LeaderId { get; set; }
    }
}
