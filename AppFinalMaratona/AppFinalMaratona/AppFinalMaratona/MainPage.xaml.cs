using AppFinalMaratona.ViewModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppFinalMaratona
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private BaseViewModel ViewModel => BindingContext as BaseViewModel;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null)
                return;
            Title = ViewModel.Title;
            ViewModel.PropertyChanged += TitlePropertyChange;
            await ViewModel.LoadAsync();
        }

        private void TitlePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ViewModel.Title))
                return;

            Title = ViewModel.Title;
        }

        private void lvwPodcast_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var viewModel = (BindingContext as MainViewModel);
            if (e.SelectedItem != null)
                viewModel.PodcastInfoCommand.Execute(e.SelectedItem);
        }
    }
}
