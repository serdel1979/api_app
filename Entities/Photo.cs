namespace api_app.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public Observation Observation { get; set; }
        public byte[] Image { get; set; }
    }
}
