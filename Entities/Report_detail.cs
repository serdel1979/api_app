namespace api_app.Entities
{
    public class Report_detail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public Report Reporte { get; set; }
        public DateTime Entry_Time { get; set; }
        public DateTime Departure_time { get; set; }
    }
}
