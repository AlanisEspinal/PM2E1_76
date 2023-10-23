using PM2E1C76.controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E1C76.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListaLugares : ContentPage
    {
        public PageListaLugares()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listaLugares.ItemsSource = await App.Instancia.GetAllLugares();
        }

        private void listaSitios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.FirstOrDefault() is Lugar lugarElegido)
            {
                double longitud = lugarElegido.Longitud;
                double latitud = lugarElegido.Latitude;

                Console.WriteLine(longitud + " " + latitud);
            }
        }

        private async void cargarLugar()
        {
            var lugares = await App.Instancia.GetAllLugares();
            listaLugares.ItemsSource= lugares;
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            if(listaLugares.SelectedItem is Lugar lugarElegido)
            {
                await App.Instancia.EliminarLugar(lugarElegido);
                await DisplayAlert("Aviso", "Lugar Eliminado", "Ok");
                cargarLugar();
            }
            else
            {
                await DisplayAlert("Aviso", "Eliga una Opcion", "Ok");
            }
        }

        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            if(listaLugares.SelectedItem is Lugar lugarElegido)
            {

                await Navigation.PushAsync(new PageMapa(lugarElegido.Longitud, lugarElegido.Latitude, lugarElegido.Descripcion));
                PageMapa maplugar = new PageMapa(lugarElegido.Longitud, lugarElegido.Latitude, lugarElegido.Descripcion);
                maplugar.pasarfoto(lugarElegido.foto);
            }
            else
            {
                await DisplayAlert("Aviso", "Seleccionoar el lugar para verlo en el mapa", "Ok");
            }
        }
    }
}