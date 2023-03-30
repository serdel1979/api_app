using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api_app.DTO
{
    public class SigninResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int Claims { get; set; }
        public DateTime Expiracion { get; set; }
    }
}