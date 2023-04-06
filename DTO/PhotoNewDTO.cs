using api_app.Entities;

namespace api_app.DTO
{
    public class PhotoNewDTO
    {
        public string Description { get; set; }
        public int ObservationId { get; set; }

        public string Image { get; set; } = "";
    }
}
