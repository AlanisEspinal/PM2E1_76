using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E1C76.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}