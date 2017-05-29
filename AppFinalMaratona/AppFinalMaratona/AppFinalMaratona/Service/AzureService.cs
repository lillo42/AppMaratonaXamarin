using AppFinalMaratona.Interface;
using AppFinalMaratona.Model;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFinalMaratona.Service
{
    public class AzureService
    {
        public const string APP_URL = "https://appfinallilloxamarin.azurewebsites.net/";
        private static object locker = new object();
        private static AzureService _instance;
        private MobileServiceClient _client;
        private IMobileServiceTable<Podcast> _podcastTable;
        public static AzureService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                            _instance = new AzureService();
                    }
                }
                return _instance;
            }
        }

        private AzureService()
        {
            _client = new MobileServiceClient(APP_URL);
            _podcastTable = _client.GetTable<Podcast>();
        }

        public async Task<List<Podcast>> GetListPodcastAsync()
        {
            return await _podcastTable
                                .ToListAsync()
                                .ConfigureAwait(false);
        }

        public async Task<MobileServiceUser> LoginAsync()
        {

            IAuthenticate auth = DependencyService.Get<IAuthenticate>();
            MobileServiceUser user = await auth.Authenticate(_client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Ops!", "Problema no Login", "Ok");
                });
            }

            return user;
        }
    }
}
