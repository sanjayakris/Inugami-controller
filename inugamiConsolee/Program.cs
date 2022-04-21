using Inugami.API;

namespace Inugami
{
    class Program
    {

        static public async void command(string cmd)
        {
            if (cmd.StartsWith("play"))
            {
                string[] acts = cmd.Split('_');
                await APICall.postRequest("PLAYING", acts[1]);
            } else if (cmd.StartsWith("stream")) 
            {
                string[] acts = cmd.Split('_');
                await APICall.postRequest("STREAMING", acts[1]);

            } else if (cmd.StartsWith("watch")) 
            {
                string[] acts = cmd.Split('_');
                await APICall.postRequest("WATCHING", acts[1]);

            } else if (cmd.StartsWith("listen"))
            {
                string[] acts = cmd.Split('_');
                await APICall.postRequest("LISTENING", acts[1]);

            }

            if (cmd.StartsWith("nick"))
            {
                string[] names = cmd.Split('_');
                await APICall.changeNickName(names[1]);
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
