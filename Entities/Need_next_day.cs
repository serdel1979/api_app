namespace api_app.Entities
{
    public class Need_next_day
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
    }
}
