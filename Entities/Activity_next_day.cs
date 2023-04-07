namespace api_app.Entities
{
    public class Activity_next_day
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
        public string Description { get; set; }
    }
}
