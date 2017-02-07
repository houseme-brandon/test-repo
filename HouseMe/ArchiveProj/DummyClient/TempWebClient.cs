using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace DummyClient
{
    public class TempWebClient
    {
        public static async Task LogOut(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);
            
            await client.PostAsync("http://localhost:55973/account/logoff", new StringContent("", Encoding.UTF8, "application/json"));
        }
    }
}
