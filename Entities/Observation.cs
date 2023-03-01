namespace api_app.Entities
{
    public class Observation
    {
        public int Id { get; set; }

        public int Id_report_detail { get; set; }
        public List<Photo> Photos { get; set; }
        public string Description { get; set; }
    }
}
