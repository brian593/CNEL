using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LaLuz.ViewModels;

public partial class BaseViewModel : ObservableObject
{
        public INavigation Navigation { get; set; }

        [ObservableProperty]
        private ImageSource foto;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private bool isBusy;

    [ObservableProperty]
    private bool isVisible;



    public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

}
