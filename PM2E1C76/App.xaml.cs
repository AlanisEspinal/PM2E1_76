using PM2E1C76.Services;
using PM2E1C76.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E1C76
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        static controladores.DBProc dBProc;

        public static controladores.DBProc Instancia
        {
            get
            {
                if(dBProc == null)
                {
                    String dbname = "Proc.db3";
                    String dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    String dbfulp = Path.Combine(dbpath, dbname);
                    dBProc = new controladores.DBProc(dbfulp);
                }
                return dBProc;
            }
        }
        protected override async void OnStart()
        {
            MainPage = new NavigationPage(new AboutPage());
            base.OnStart();

            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if(status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if(status != PermissionStatus.Granted)
            {
                Console.WriteLine("Los permisos estan concedidos");
            }
            else
            {
                Console.WriteLine("El usuario denego los permisos");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
