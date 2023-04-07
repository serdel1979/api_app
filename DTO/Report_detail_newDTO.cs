using Microsoft.AspNetCore.SignalR;

namespace api_app.DTO
{
    public class Report_detail_newDTO
    {
        public string UserId { get; set; }
        public int ReportId { get; set; }
        public string Entry_time { get; set; }
        public string Departure_time { get; set; }
    }
}
