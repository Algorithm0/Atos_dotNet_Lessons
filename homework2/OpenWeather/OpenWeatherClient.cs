using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace homework2.OpenWeather
{
	public class OpenWeatherClient
	{
		private const string DefaultLanguage = "ru";
		private const string ApiKey = "6c2374a8dd9bd43b6ce06dbc94b2a896";
		private const string UrlTemplate = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&lang={2}";

		// Will be used later
		// const string iconUrlTemplate = "http://openweathermap.org/img/w/{0}.png";

		private readonly Dictionary<string, CurrentWeatherDto> _cache = new Dictionary<string, CurrentWeatherDto>();

		public async ValueTask<CurrentWeatherDto> GetWeatherAsync(string cityName)
		{
			var lowerCasedCityName = cityName.ToLower();

			if (_cache.ContainsKey(lowerCasedCityName))
			{
				return _cache[lowerCasedCityName];
			}

			var currentWeatherUrl = string.Format(UrlTemplate, lowerCasedCityName, ApiKey, DefaultLanguage);
			var httpClient = new HttpClient();

			var response = await httpClient.GetAsync(currentWeatherUrl);
			if (!response.IsSuccessStatusCode)
				throw new Exception($"Openweathermap response has a fault code {response.StatusCode}");
			var currentWeatherJson = await response.Content.ReadAsStringAsync();

			var currentWeatherDocument = JsonDocument.Parse(currentWeatherJson);
			var currentWeatherDto = currentWeatherDocument.Deserialize<CurrentWeatherDto>();
			_cache[lowerCasedCityName] = currentWeatherDto;
			return currentWeatherDto;
		}
	}
}
