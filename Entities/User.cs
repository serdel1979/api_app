using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_app.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Dni { get; set; }
        public Responsability Responsability { get; set; }
        public int ResponsabilityId { get; set; }
        public Boolean Leader { get; set; } = false;
    }
}