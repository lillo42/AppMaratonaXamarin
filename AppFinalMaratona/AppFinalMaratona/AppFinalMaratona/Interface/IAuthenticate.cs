using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace AppFinalMaratona.Interface
{
    public interface IAuthenticate
    {
        Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider);
    }
}
