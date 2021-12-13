using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace homeworks
{
	public static class Program
	{
		private const string ApiKey = "6c2374a8dd9bd43b6ce06dbc94b2a896";
		
		public static async Task<int> Main()
		{
			//double[] cord = GetLatLonFromInput();
			double[] cord = await GetLatLonFromData(GetCityFromInput());

			 var builder = new UriBuilder("https://api.openweathermap.org/data/2.5/onecall");
			 var queryParameters = HttpUtility.ParseQueryString(builder.Query);
			 queryParameters["exclude"] = "minutely,hourly,daily,alerts";
			 queryParameters["lat"] = cord[0].ToString(CultureInfo.CurrentCulture);
			 queryParameters["lon"] = cord[1].ToString(CultureInfo.CurrentCulture);
			 queryParameters["appid"] = ApiKey;
			 builder.Query = queryParameters.ToString();
			 Uri uri = builder.Uri;
			 var request = new HttpRequestMessage(HttpMethod.Get, uri);
			 var client = new HttpClient();
			 HttpResponseMessage result = await client.SendAsync(request);
			 result.EnsureSuccessStatusCode();
			
			 string jsonContent = await result.Content.ReadAsStringAsync();
			 JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
			 var kelvinDegrees = jsonDocument.RootElement
			 	.GetProperty("current")
			 	.GetProperty("temp")
			 	.GetDouble();
			
			 var dateUnixTimeSeconds = jsonDocument.RootElement
			 	.GetProperty("current")
			 	.GetProperty("dt")
			 	.GetInt32();
			
			 var timeOffset = jsonDocument.RootElement
			 	.GetProperty("timezone_offset")
			 	.GetInt32();
			
			 var offsetUtc = DateTimeOffset.FromUnixTimeSeconds(dateUnixTimeSeconds);
			 var offsetCord = offsetUtc.ToOffset(TimeSpan.FromSeconds(timeOffset));
			
			 Console.WriteLine("Temp in celsius: {1}, date: {0}", offsetCord, KelvinToCelsius(kelvinDegrees));

			cord = null!;
			return 0;
		}

		private static double KelvinToCelsius(double kelvinDegrees)
		{
			return kelvinDegrees - 273.15;
		}

		private static string GetCityFromInput(string helloMessage = "Enter city, please:")
		{
			Console.WriteLine(helloMessage); 
			return Console.ReadLine() ?? string.Empty;
		}

		private static double ValidInputDouble(string helloMessage, string errorMessage = "Invalid format, please input again!")
		{
			double res;
			Console.WriteLine(helloMessage);
			while (!double.TryParse(Console.ReadLine(), out res))
			{
				Console.WriteLine(errorMessage);
			}
			Console.Clear();
			return res;
		}

		private static double[] GetLatLonFromInput()
		{
			double[] cord = new double[2];
			cord[0] = ValidInputDouble("Enter latitude:");
			cord[1] = ValidInputDouble("Enter longitude:");
			return cord;
		}

		private static async Task<double[]> GetLatLonFromData(string city)
		{
			double[] cord = new double[2];

			var builder = new UriBuilder("https://api.openweathermap.org/data/2.5/weather");
			var queryParameters = HttpUtility.ParseQueryString(builder.Query);
			queryParameters["q"] = city;
			queryParameters["appid"] = ApiKey;
			builder.Query = queryParameters.ToString();
			Uri uri = builder.Uri;
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			var client = new HttpClient();
			HttpResponseMessage result = await client.SendAsync(request);
			result.EnsureSuccessStatusCode();
			
			string jsonContent = await result.Content.ReadAsStringAsync();
			JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
			cord[0] = jsonDocument.RootElement
				.GetProperty("coord")
				.GetProperty("lat")
				.GetDouble();
			cord[1] = jsonDocument.RootElement
				.GetProperty("coord")
				.GetProperty("lon")
				.GetDouble();
			return cord;
		}
	}
}