using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChefConnect.RazorPageWebApp.Pages
{
    public class TestModel : PageModel
    {
        public List<WeatherForecast> WeatherForecasts { get; set; } = new List<WeatherForecast>();

        private readonly HttpClient _httpClient;

        public TestModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            try
            {
                // Adjust the URL to match the endpoint of your API
                var response = await _httpClient.GetAsync("https://localhost:7212/WeatherForecast");
                if (response.IsSuccessStatusCode)
                {
                    WeatherForecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
                }
            }
            catch (Exception ex)
            {
                // Log or handle errors here
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
