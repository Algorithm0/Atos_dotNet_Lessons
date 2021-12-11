using System;
using System.Net.Http;
using System.Runtime.Caching;
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

		private readonly ObjectCache _cache = MemoryCache.Default;

		public async ValueTask<CurrentWeatherDto> GetWeatherAsync(string cityName)
		{
			var lowerCasedCityName = cityName.ToLower();

			//если находятся данные по ключу данного города, то вернуть их
			if ((_cache[lowerCasedCityName] is CurrentWeatherDto currentWeatherDto)) return currentWeatherDto;
			
			var currentWeatherUrl = string.Format(UrlTemplate, lowerCasedCityName, ApiKey, DefaultLanguage);
			var httpClient = new HttpClient();

			var response = await httpClient.GetAsync(currentWeatherUrl);
			if (!response.IsSuccessStatusCode)
				throw new Exception($"OpenWeatherMap response has a fault code {response.StatusCode}");
			var currentWeatherJson = await response.Content.ReadAsStringAsync();
			var currentWeatherDocument = JsonDocument.Parse(currentWeatherJson);
			currentWeatherDto = currentWeatherDocument.Deserialize<CurrentWeatherDto>();
			
			//обновляем политику, указываем, что через сейчас+10мин запись должна быть удалена
			var policy = new CacheItemPolicy()
			{
				AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(10)
			};
			//добавление записи в cache с обработкой исключения на NULL
			_cache.Set(lowerCasedCityName, currentWeatherDto ?? throw new InvalidOperationException("Getting Date from API is NULL"), policy);
			return currentWeatherDto;
		}
	}
}
