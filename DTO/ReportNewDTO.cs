namespace api_app.DTO
{
    public class ReportNewDTO
    {
        public int ProjectId { get; set; }
        public Report_detail_newDTO[] Detail { get; set; }
        public string Report { get; set; }
    }
}
