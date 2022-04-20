using System.Text;
using System.Text.Json;

namespace InugamiConsole
{
    class Program
    {
        class ClientActivity
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }

        class ClientName
        {
            public string Name { get; set; }
        }

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

        static public async void command(string cmd)
        {
            if (cmd.StartsWith("play"))
            {
                string[] acts = cmd.Split('_');
                await postRequest("PLAYING", acts[1]);
            } else if (cmd.StartsWith("stream")) 
            {
                string[] acts = cmd.Split('_');
                await postRequest("STREAMING", acts[1]);

            } else if (cmd.StartsWith("watch")) 
            {
                string[] acts = cmd.Split('_');
                await postRequest("WATCHING", acts[1]);

            } else if (cmd.StartsWith("listen"))
            {
                string[] acts = cmd.Split('_');
                await postRequest("LISTENING", acts[1]);

            }

            if (cmd.StartsWith("nick"))
            {
                string[] names = cmd.Split('_');
                await changeNickName(names[1]);
            }
        }
        static public async Task Main()
        {
            int num = 1;
            
            do
            {
                string act = Console.ReadLine();
                command(act);
            } while (num == 1);
        }
    }
}
