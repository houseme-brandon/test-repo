using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccessControl.Services
{
    public static class UrlShortener
    {
        public static string ShortenUrl(string longUrl)
        {
            var client = new HttpClient { BaseAddress = new Uri("https://www.googleapis.com") };
           
            var content = new StringContent("{\"longUrl\":\"" + longUrl + "\"}",Encoding.UTF8,
                "application/json");

            var test = JsonConvert.DeserializeObject<UrlShortenerMessage>(
                    client.PostAsync("/urlshortener/v1/url?key=AIzaSyAqt5WRGeaa3XlVKThQoatHuQDwqOl-taM", content)
                        .Result.Content.ReadAsStringAsync().Result);
            return test.id;            
        }
    }
}