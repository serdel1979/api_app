namespace api_app.Entities
{
    public class Report
    {
        int Id { get; set; }
        int Id_project { get; set; }
        public Project Project { get; set; }
        public List<Developed_Activity>  Developed_activities { get; set; }


        public List<Observation> Observations { get; set; }

        public List<Need_next_day> Needs_next_day { get; set; }
    }
}
