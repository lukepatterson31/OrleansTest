using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using GrainInterfaces;

namespace OrleansTest
{
    public class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                { 
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }

            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansTest";

                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("\nClient successfully connected to silo host\n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            var honeybadger = client.GetGrain<IHoneybadger>(1);
            var commandResult = await honeybadger.FlipFlag("flagpath");
            Console.Write($"Honeybadger: returned {commandResult} exited with code: {commandResult.ExitCode}");
            
            var friend = client.GetGrain<IHello>(0);
            var response = await friend.SayHello("Good morning, HelloGrain!");
            Console.WriteLine($"\n\n{response}\n\n");
        }
    }
}
