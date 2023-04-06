namespace api_app.Entities
{
    public class Observation
    {
        public int Id { get; set; }

        public Report_detail Report_detail { get; set; }
        public int Report_detailId { get; set; }
        public List<Photo> Photos { get; set; }
        public string Description { get; set; }
    }
}
