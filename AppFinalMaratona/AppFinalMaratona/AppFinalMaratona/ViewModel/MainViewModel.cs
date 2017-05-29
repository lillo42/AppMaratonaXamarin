using AppFinalMaratona.Model;
using AppFinalMaratona.Service;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppFinalMaratona.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Podcast> _podcast;

        public ObservableCollection<Podcast> Podcasts
        {
            get => _podcast;
            set => SetProperty(ref _podcast, value);
        }

        private string _search;

        public string Search
        {
            get => _search;
            set => SetProperty(ref _search, value);
        }


        public ICommand LoadCommand { get; }
        public ICommand SearchComamnd { get; }
        public ICommand AboutCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand PodcastInfoCommand { get; set; }

        public MainViewModel()
        {
            LoadCommand = new Command(ExecuteLoadCommand);
            SearchComamnd = new Command(ExecuteSearchCommand);
            AboutCommand = new Command(ExecuteAboutCommand);
            LoginCommand = new Command(ExecuteLoginCommand);
            PodcastInfoCommand = new Command<Podcast>(ExecutePodcastCommand);
            Title = "Home";
        }

        public override Task LoadAsync()
        {
            ExecuteLoadCommand();
            return base.LoadAsync();
        }

        private async void ExecuteLoadCommand()
        {
            if (Loading)
                return;

            try
            {
                Podcasts = new ObservableCollection<Podcast>(await AzureService.Instance.GetListPodcastAsync());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            finally
            {
                Loading = false;
            }

        }

        private async void ExecuteSearchCommand()
        {
            if (Loading)
                return;
            Loading = true;
            try
            {
                var list = await AzureService.Instance.GetListPodcastAsync();
                Podcasts = new ObservableCollection<Podcast>(list.Where(x => x.Titulo.Contains(Search) || x.SubTitulo.Contains(Search)));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            finally
            {
                Loading = false;
            }
        }

        private async void ExecuteAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        private async void ExecuteLoginCommand()
        {
            MobileServiceUser user = await AzureService.Instance.LoginAsync();
            if (user != null)
                Title = $"Home - Bem vindo {user.UserId}";
        }

        private async void ExecutePodcastCommand(Podcast podcast)
        {
            await PushAsync<ContentWebViewModel>(podcast);
        }
    }
}
