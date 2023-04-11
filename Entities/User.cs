using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_app.Entities
{
    public class User : IdentityUser
    {
   
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Dni { get; set; }
        public Responsability Responsability { get; set; }
        public int? ResponsabilityId { get; set; }
        public List<Assigned_Activity> Assigned_Activities { get; set; }

        public Project Project { get; set; }

        public int? ProjectId { get; set; }
        public Boolean IsAdmin { get; set; } = false;
        public Boolean Leader { get; set; } = false;
    }
}