namespace api_app.DTO
{
    public class ReportNewDTO
    {
        public int ProjectId { get; set; }
        public Report_detail_newDTO[] Detail { get; set; }
        public Developed_ActivityNewDTO[] Activities_developed { get; set; }

        public Activity_next_dayNewDTO[] Activity_to_Dev { get; set; }
        public Need_next_dayNewDTO[] Need_next_day { get; set; }
        public string Report { get; set; }
    }
}
