using Microsoft.AspNetCore.Identity;

namespace api_app.Entities
{
    public class User : IdentityUser
    {
        public Boolean Leader { get; set; } = false;
        public int Id_Responsibility { get; set; }
    }
}