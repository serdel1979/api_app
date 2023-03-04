using api_app.Entities;

namespace api_app.DTO
{
    public class ProjectNewDTO
    {
        public string Name { get; set; }
        public string Client { get; set; }
        public int JobId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Construction_manager { get; set; }
        public string Supervisor { get; set; }
        public string Reference { get; set; }
        public int LeaderId { get; set; }
    }
}
