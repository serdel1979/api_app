namespace api_app.Entities
{
    public class Observation
    {
        public int Id { get; set; }

        public Report Report { get; set; }
        public List<Photo> Photos { get; set; }
        public string Description { get; set; }
    }
}
