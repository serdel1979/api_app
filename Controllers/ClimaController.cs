using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace api_app.Controllers
{

    [ApiController]
    [Route("clima")]
    public class ClimaController : ControllerBase
    {


        public ClimaController(IConfiguration Configuration, ) {
            this.Configuration = Configuration;

            this._client = new HttpClient();
        }

        public IConfiguration Configuration { get; }

        private HttpClient _client;

        [HttpGet("/lat/{lat}/longitud/{longitud}")]
        public async Task<ActionResult> clime(string lat, string longitud)
        {
            try
            {
                var key = Configuration.GetValue<string>("ApiKey");
                var response = await _client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={longitud}&appid={key}&lang=es");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"latitud {lat} y {longitud} recibida");

                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }



    }
}
