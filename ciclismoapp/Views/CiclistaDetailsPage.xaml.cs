
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ciclismoapp.Models;
using ciclismoapp.Services;
using ciclismoapp.ViewModels;
using Xamarin.Essentials;
using Plugin.Geolocator;




namespace ciclismoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CiclistaDetailsPage : ContentPage
    {
        DBFirebase services;
        public CiclistaDetailsPage(Ciclista ciclista)
        {
           
            InitializeComponent();
            BindingContext = new LocalizacionViewModel(ciclista);

            services = new DBFirebase();



        }

        public async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var localizacion = args.Item as Localizacion;
            if (localizacion == null) return;

            MapLaunchOptions options = new MapLaunchOptions { Name = "Posición actual." };
            await Map.OpenAsync(localizacion.Latitud, localizacion.Longitud, options);

            lstCiclistas.SelectedItem = null;
        }





    }
}