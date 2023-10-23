using Plugin.Geolocator;
using Plugin.Media;
using PM2E1C76.controladores;
using SQLite;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E1C76.Views
{
    public partial class AboutPage : ContentPage
    {

        Plugin.Media.Abstractions.MediaFile fotom = null;

        private SQLiteAsyncConnection _database;
        public AboutPage()
        {
            InitializeComponent();
            var locator = CrossGeolocator.Current;
            bool gpsEnable = locator.IsGeolocationEnabled;

            if (gpsEnable)
            {
                GetLocation();
                _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lugares.db3"));
                _database.CreateTableAsync<Lugar>().Wait();

            }
            else
            {
                DisplayAlert("Aviso", "GPS desactivado, salga de la pp y active el GPS ", "Ok");                
            }
        }

        private async void GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if(location != null)
            {
                EntryLongitud.Text = location.Longitude.ToString();
                EntryLatitud.Text = location.Latitude.ToString();
            }
            else
            {
                await DisplayAlert("Aviso", "No se obtuvo Ubicación", "Ok");
            }

        }

        public String GetImage64()
        {
            if(fotom != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = fotom.GetStream();
                    stream.CopyTo(ms); 
                    byte[] fotobytes = ms.ToArray();

                    String Base64 = Convert.ToBase64String(fotobytes);

                    return Base64;
                }
            }
            return null;
        }

        public byte[] GetImageByte()
        {
            if(fotom != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = fotom.GetStream();
                    stream.CopyTo(ms);
                    byte[] fotobytes = ms.ToArray();

                    return fotobytes;
                }
            }
            return null;
        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            fotom = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MyApp",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if(fotom != null)
            {
                foto.Source = ImageSource.FromStream(() => { return fotom.GetStream(); });  
            }

        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            Validar valido = new Validar();
            bool validarCampos = valido.validarcampo(foto.Source, EntryLongitud.Text, EntryLatitud.Text, EntryDescipcion.Text);

            if (validarCampos)
            {
                var lugar = new controladores.Lugar
                {
                    Longitud = double.Parse(EntryLongitud.Text),
                    Latitude = double.Parse(EntryLatitud.Text),
                    Descripcion = EntryDescipcion.Text,
                    foto = GetImageByte()
                };

                if(await App.Instancia.AggLugar(lugar) > 0)
                {
                    await DisplayAlert("Aviso", "Registro exitoso", "Ok");

                    foto.Source = null;
                    EntryDescipcion.Text= string.Empty;
                }
                else
                {
                    await DisplayAlert("Aviso", "A ocurrido un error", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Campos vacio, LLene todos los campos", "Ok");
            }
        }

        private async void btnlista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageListaLugares());
        }

        private void btnsalir_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}