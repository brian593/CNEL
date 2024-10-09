using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LaLuz.ViewModels;

public partial class BaseViewModel : ObservableObject
{
        public INavigation Navigation { get; set; }

        // Propiedad automática con notificación de cambios
        [ObservableProperty]
        private ImageSource foto;

        // Propiedad automática con notificación de cambios para el título
        [ObservableProperty]
        private string title;

        // Propiedad automática con notificación de cambios para el estado de carga
        [ObservableProperty]
        private bool isBusy;

    [ObservableProperty]
    private bool isVisible;



    // Métodos para mostrar alertas
    public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

}
