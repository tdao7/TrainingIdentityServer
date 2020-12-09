using System.Threading.Tasks;
using IdentityServer.Models.Form;

namespace IdentityServer.RestApis.Services
{
    public interface IUserServices
    {
        Task<string> login(LoginForm loginForm);
    }
}