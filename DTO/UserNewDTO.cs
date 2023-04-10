using api_app.Entities;

namespace api_app.DTO
{
    public class UserNewDTO
    {
        public string Email { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Dni { get; set; }
        public int ResponsabilityId { get; set; }

        public int ProjectId { get; set; }
        public Boolean Leader { get; set; } = false;
    }
}
