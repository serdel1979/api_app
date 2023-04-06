using api_app.Entities;

namespace api_app.DTO
{
    public class ObservationNewDTO
    {
        public int Report_detailId { get; set; }
        public string Description { get; set; }
        public PhotoNewDTO[] Photos { get; set; }
    }
}
