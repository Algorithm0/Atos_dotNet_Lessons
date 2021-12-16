using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using homework2.OpenWeather;
using Microsoft.AspNetCore.Mvc;

namespace homework2.Controllers
{
    [Route("weather")]
    public class WeatherController : Controller
    {
        private readonly OpenWeatherClient _openWeatherClient;

        public WeatherController(OpenWeatherClient openWeatherClient)
        {
            _openWeatherClient = openWeatherClient;
        }
		
        public async Task<ActionResult> Get([Required, FromQuery(Name = "city")]string cityName)
        {
            if (!ModelState.IsValid)
                return BadRequest("Name of a city is not provided");
		
            var currentWeatherDto = await _openWeatherClient.GetWeatherAsync(cityName);
		
            return Ok(currentWeatherDto);
        }
    }
}
