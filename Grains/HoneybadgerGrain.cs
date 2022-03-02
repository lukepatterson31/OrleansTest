using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using GrainInterfaces;
using Microsoft.Extensions.Logging;

namespace Grains
{
    public class HoneybadgerGrain : Orleans.Grain, IHoneybadger
    {
        private readonly ILogger _logger;

        public HoneybadgerGrain(ILogger<HoneybadgerGrain> logger)
        {
            _logger = logger;
        }

        async Task<CommandResult> IHoneybadger.FlipFlag(string flagPath)
        {
            _logger.LogInformation("executing command!");
            var result = await Cli.Wrap("aws")
                .WithArguments($"ssm get-parameters --names {flagPath}")
                .ExecuteBufferedAsync();
            _logger.LogInformation("Honeybadger Parameters: {Returnedparameters}", result.StandardOutput);
            return result;
        }
    }
}