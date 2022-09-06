using ciclismoapp.Views.AccessApp;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ciclismoapp.ViewModels
{
    internal class Register : BaseViewModel
    {
        FirebaseClient firebaseCliente = new FirebaseClient("https://ciclismoapp-60fa5-default-rtdb.firebaseio.com/");

        #region Attributes
        public string email;
        public string password;
        public string name;
        public string age;
        public string team;
        public string cell;
        public double latitud;
        public double longiutd;

        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Properties
        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string PasswordTxt
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public string NameTxt
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }

        public string AgeTxt
        {
            get { return this.age; }
            set { SetValue(ref this.age, value); }
        }

        public string NameTeamTxt
        {
            get { return this.team; }
            set { SetValue(ref this.team, value); }
        }
        public string CellTxt
        {
            get { return this.cell; }
            set { SetValue(ref this.cell, value); }
        }
        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        #endregion

        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(RegisterMethod);
            }
        }
        #endregion



        #region Methods



        private async void RegisterMethod()
        {
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Es necesario ingresar un correo electrónico.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Es necesario ingresar una clave personal.",
                    "Aceptar");
                return;
            }

            string WebAPIkey = "AIzaSyBf7Y3G6mfUvZ31X1W1SvnKjhHhX8ugYkk";

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                string gettoken = auth.FirebaseToken;

                await authProvider.SendEmailVerificationAsync(gettoken);

                await firebaseCliente
                .Child("Cyclists")
                .Child(auth.User.LocalId)
                .Child("PersonalInfo")
                .PostAsync(new { email = EmailTxt, team = NameTeamTxt, cellPhone = CellTxt, age = AgeTxt, completeName = NameTxt, uid = auth.User.LocalId});


                await firebaseCliente
                .Child("Users")
                .PostAsync(new { email = EmailTxt, team = NameTeamTxt, cellPhone = CellTxt, age = AgeTxt, completeName = NameTxt, uid = auth.User.LocalId });


                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }


        }
        #endregion

        #region Constructor
        public Register()
        {
            IsEnabledTxt = true;
        }
        #endregion

    }
}
