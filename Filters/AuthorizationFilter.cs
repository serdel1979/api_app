using api_app.DTO;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace api_app.Filter
{
    public class AuthorizationFilter : IAuthorizationFilter
    {



        public void OnAuthorization(AuthorizationFilterContext context)
        {

            string token = context.HttpContext.Request.Headers["x-google-token"];
            _ = ValidateTokenAsync(token, context);

        }

        public async Task ValidateTokenAsync(string token, AuthorizationFilterContext context)
        {

            try
            {
                
                var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { "402562450789-0aau8bfeu40ef95tg4c1c8trnrjoblo5.apps.googleusercontent.com" }
                });

            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }

        }
    }
}
