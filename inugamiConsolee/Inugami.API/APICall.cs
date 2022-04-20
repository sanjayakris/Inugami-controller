using System.Text;
using System.Text.Json;
using Inugami.Client;

namespace Inugami.API
{
    internal class APICall
    {

        static public HttpClient client = new HttpClient();

        static public async Task getRequest()
        {
            var response = await client.GetStringAsync("http://localhost:5600/log");
            Console.WriteLine(response);
        }

        static public async Task postRequest(string type, string name)
        {
            var activity = new ClientActivity
            {
                Name = name,
                Type = type
            };

            var stringed = JsonSerializer.Serialize(activity);
            var content = new StringContent(stringed, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5600/activity", content);
            response.EnsureSuccessStatusCode();

            string res = await response.Content.ReadAsStringAsync();
            Console.WriteLine(res);
        }

        static public async Task changeNickName(string name)
        {
            var nick = new ClientName
            {
                Name = name
            };

            var stringed = JsonSerializer.Serialize(nick);
            var content = new StringContent(stringed, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5600/nickname", content);
            response.EnsureSuccessStatusCode();

            string res = await response.Content.ReadAsStringAsync();
            Console.WriteLine(res);
        }
    }
}
