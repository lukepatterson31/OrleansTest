using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger _logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            _logger = logger;
        }
        
        Task<string> IHello.SayHello(string greeting)
        {
            _logger.LogInformation($"\n Say hello message received: greeting = '{greeting}'");
            return Task.FromResult($"\n Client said: {greeting}, so HelloGrain says: Hello!");
        }
    }
}