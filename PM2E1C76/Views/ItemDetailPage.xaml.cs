using PM2E1C76.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PM2E1C76.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}