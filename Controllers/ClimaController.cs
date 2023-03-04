using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace api_app.Controllers
{

    [ApiController]
    [Route("climate")]
    public class ClimaController : ControllerBase
    {


        public ClimaController(IConfiguration Configuration) {
            this.Configuration = Configuration;

            this._client = new HttpClient();
        }

        public IConfiguration Configuration { get; }

        private HttpClient _client;

        [HttpGet("/latitud/{latitud}/longitud/{longitud}")]
        public async Task<ActionResult> climate(string latitud, string longitud)
        {
            try
            {
                var key = Configuration.GetValue<string>("ApiKey");
                var response = await _client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={latitud}&lon={longitud}&appid={key}&lang=es");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"latitud {latitud} y {longitud} recibida");

                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }



    }
}
