
using Firebase.Database;
using System.Collections.ObjectModel;
using ciclismoapp.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Auth;

using Plugin.Geolocator;
using Xamarin.Essentials;

namespace ciclismoapp.Services
{
    public class DBFirebase
    {
        FirebaseClient client;
        string webAPIKey = "AIzaSyBf7Y3G6mfUvZ31X1W1SvnKjhHhX8ugYkk";
        FirebaseAuthProvider authProvider;


        public DBFirebase()
        {
            client = new FirebaseClient("https://ciclismoapp-60fa5-default-rtdb.firebaseio.com/");
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));

        }
        public ObservableCollection<Ciclista> getCiclistas()
        {
            var ciclistasData = client
                  .Child("/Users")
                  .AsObservable<Ciclista>()
                  .AsObservableCollection();


            return ciclistasData;
        }
        public ObservableCollection<Localizacion> getLocalizacion(Ciclista ciclista)
        {
            var ciclistasLocation = client
                  .Child("/Cyclists/"+ciclista.Uid+"/Localization")
                  .AsObservable<Localizacion>()
                  .AsObservableCollection();


            return ciclistasLocation;
        }

        public async Task UpdateCiclista(string completeName, string team, string age, string uid)
        {
            var toUpdateCiclista = (await client
                  .Child("/Cyclists/BjrJJBMDLxPZPd4v8YmpALAaLkp1/Localization")
                    .OnceAsync<Localizacion>()).FirstOrDefault
                    (a => a.Object.Uid == uid);
        }

        public async Task<bool> ResetPassword(string email)
        {
            Console.WriteLine("entra al reset password");
            await authProvider.SendPasswordResetEmailAsync(email);
            return true;
        }




    }
}
