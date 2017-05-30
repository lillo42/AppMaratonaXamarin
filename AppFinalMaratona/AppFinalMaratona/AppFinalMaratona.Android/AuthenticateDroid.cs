using AppFinalMaratona.Droid;
using AppFinalMaratona.Interface;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticateDroid))]
namespace AppFinalMaratona.Droid
{
    public class AuthenticateDroid : IAuthenticate
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {
                return await client.LoginAsync(Forms.Context, provider);
            }
            catch
            {
                return null;
            }
        }
    }
}