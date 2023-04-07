namespace api_app.Entities
{
    public class Report_detail
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
        public DateTime Entry_Time { get; set; }
        public DateTime Departure_time { get; set; }
    }
}
