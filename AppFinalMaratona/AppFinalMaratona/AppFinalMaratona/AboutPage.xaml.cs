using AppFinalMaratona.ViewModel;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinalMaratona
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
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
    }
}