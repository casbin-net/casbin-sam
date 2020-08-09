using System.Threading.Tasks;

namespace Casbin.Sam.Abstractions
{
    public interface IVersionTokenValidator<in TSource, in TToken>
    {
        public Task<bool> ValidateAsync(TSource source, TToken exceptToken);
    }
}
