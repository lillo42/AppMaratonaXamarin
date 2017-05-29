using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppFinalMaratona.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool _loading;

        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value);
        }


        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChange([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChange(propertyName);
            return true;
        }

        public async Task PushAsync<TViewModel>(params object[] args)
            where TViewModel : BaseViewModel
        {
            try
            {
                Type viewModelType = typeof(TViewModel);
                string viewModelTypeName = viewModelType.Name;
                int viewModelWordLength = "ViewModel".Length;
                string viewTypeName = $"AppFinalMaratona.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}Page";
                Type viewType = Type.GetType(viewTypeName);
                Page page = Activator.CreateInstance(viewType) as Page;

                var viewModel = Activator.CreateInstance(viewModelType, args);
                if (page != null)
                    page.BindingContext = viewModel;
                await App.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
        }

        public virtual Task LoadAsync()
        {
            return Task.FromResult(0);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task DisplayAlert(string title, string message, string accept, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
