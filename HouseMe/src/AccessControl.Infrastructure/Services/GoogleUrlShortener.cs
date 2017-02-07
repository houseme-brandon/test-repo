using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace AccessControl.Infrastructure.Services
{
    public class UrlShortenerMessage
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("longUrl")]
        public string LongUrl { get; set; }
    }

    public class GoogleUrlShortener
    {
        public static string ShortenUrl(string longUrl)
        {
            var client = new HttpClient { BaseAddress = new Uri("https://www.googleapis.com") };

            var content = new StringContent("{\"longUrl\":\"" + longUrl + "\"}", Encoding.UTF8,
                "application/json");

            var test = JsonConvert.DeserializeObject<UrlShortenerMessage>(
                    client.PostAsync("/urlshortener/v1/url?key=AIzaSyAqt5WRGeaa3XlVKThQoatHuQDwqOl-taM", content)
                        .Result.Content.ReadAsStringAsync().Result);
            return test.Id;
        }
    }
}
