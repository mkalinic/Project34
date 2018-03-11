using System.Security.Claims;
using System.Threading.Tasks;

namespace IGG.TenderPortal.WebService.Models
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}