using System.Threading.Tasks;

namespace Casbin.Sam.Abstractions
{
    public interface IVersionTokenProvider<in TSource, TToken>
    {
        public Task<TToken> GenerateVersionTokenAsync(TSource source);
    }
}
