namespace api_app.Entities
{
    public class Report
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public Project Project { get; set; }
        public List<Report_detail> Report_Detail { get; set; }

        public List<Developed_Activity>  Developed_activities { get; set; }


        public List<Observation> Observations { get; set; }

        public List<Need_next_day> Needs_next_day { get; set; }
        public List<Activity_next_day> Activities_next_day { get; set; }
    }
}
