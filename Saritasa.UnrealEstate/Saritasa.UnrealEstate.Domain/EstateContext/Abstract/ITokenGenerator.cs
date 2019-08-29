using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Abstract
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(string username, string password);
    }
}
