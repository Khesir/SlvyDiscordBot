using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SlvyDiscordBot.Config
{
    public class TenorApi
    {
        private readonly string _apiKey;
        private readonly HttpClient httpClient;

        public TenorApi(string apiKey)
        {
            this._apiKey = apiKey;
            this.httpClient = new HttpClient();
        }

        public async Task<TenorApiResponse> SearchGifs(string query, int limit)
        {

            string apiUrl = $"https://api.tenor.com/v1/search?q={query}&key={_apiKey}&limit={limit}";

            try
            {
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TenorApiResponse>(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching GIFs: {ex.Message}");
                return null;
            }
        }
    }

    public class TenorApiResponse
    {
        public List<TenorGif> Results { get; set; }
    }

    public class TenorGif
    {
        public List<TenorMedia> Media { get; set; }
    }

    public class TenorMedia
    {
        public TenorGifData Gif { get; set; }
    }

    public class TenorGifData
    {
        public string Url { get; set; }
    }
}
