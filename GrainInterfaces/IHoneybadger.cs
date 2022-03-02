using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;

namespace GrainInterfaces
{
    public interface IHoneybadger : Orleans.IGrainWithIntegerKey
    {
        Task<CommandResult> FlipFlag(string flagPath);
    }
}