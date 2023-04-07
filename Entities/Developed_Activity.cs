namespace api_app.Entities
{
    public class Developed_Activity
    {
        public int Id { get; set; }

        public int ReportId { get; set; }
        public Report Report { get; set; }
        public string Description { get; set; }
        public List<Assigned_Activity> AssignedActivities { get; set; }
    }
}
