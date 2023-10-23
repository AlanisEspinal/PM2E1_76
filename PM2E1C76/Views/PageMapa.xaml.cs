using ImageFromXamarinUI;
using Plugin.Geolocator;
using PM2E1C76.controladores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E1C76.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMapa : ContentPage
    {
        Lugar lugar = new Lugar();

        public PageMapa(double longitud, double latitud, string descripcion)
        {
            InitializeComponent();

            var locator = CrossGeolocator.Current;
            bool isGpsEnable = locator.IsGeolocationEnabled;

            if (isGpsEnable)
            {
                DisplayAlert("Aviso", "GPS Activado", "Ok");
                var pin = new Pin
                {
                    Position = new Position(longitud, latitud),
                    Label = "Pin: " + descripcion
                };

                mapalugar.Pins.Add(pin);
                mapalugar.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMeters(100)));

            }
            else
            {
                DisplayAlert("Aviso", "GPS Desactivado", "Ok");
            }
        }
        public async void pasarfoto(byte[] foto)
        {
            if(foto !=null)
            {
                var imglugar = foto;
                var carpPath = Path.Combine(FileSystem.CacheDirectory, "lugar.png");
                using (var fileStream = File.OpenWrite(carpPath))
                {
                    await fileStream.WriteAsync(imglugar, 0, imglugar.Length);
                }
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Foto del lugar",
                    File = new ShareFile(carpPath)
                });

            }
            else
            {
                await DisplayAlert("Aviso", "No existe foto disponible", "OK");
            }
        }
        private async void btncompartirUbi_Clicked(object sender, EventArgs e)
        {
            var mapafoto = await mapa.CaptureImageAsync();

            var filePath = Path.Combine(FileSystem.CacheDirectory, "mapa.png");
            using (var fileStream = File.OpenWrite(filePath))
            {
                await mapafoto.CopyToAsync(fileStream);
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Caprutar de pantalla del mapa",
                File = new ShareFile(filePath)
            });
        }
    }
}