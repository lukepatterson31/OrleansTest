using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IHoneybadger : Orleans.IGrainWithIntegerKey
    {
        Task<string> FlipFlag(string flagPath, GrainCancellationToken gct);
    }
}