using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using CliWrap;
using CliWrap.Buffered;
using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Grains
{
    public class HoneybadgerGrain : Orleans.Grain, IHoneybadger
    {
        private readonly ILogger _logger;

        public HoneybadgerGrain(ILogger<HoneybadgerGrain> logger)
        {
            _logger = logger;
        }

        async Task<string> IHoneybadger.FlipFlag(string flagPath, GrainCancellationToken gct)
        {
            string returnedParams = string.Empty;
            if (!gct.CancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("executing command!");
                
                var cmdResult = await Cli.Wrap("aws")
                    .WithArguments($"ssm get-parameters --names {flagPath}")
                    .ExecuteBufferedAsync(gct.CancellationToken);

                returnedParams = cmdResult.StandardOutput;
                _logger.LogInformation("Honeybadger Parameters: {Returnedparameters}", returnedParams);
            }
            return returnedParams;
        }
    }
}