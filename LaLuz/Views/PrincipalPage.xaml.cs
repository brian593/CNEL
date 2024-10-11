using LaLuz.Models;
using LaLuz.ViewModels;

namespace LaLuz.Views;

public partial class PrincipalPage : ContentPage
{
    MainPageViewModel _viewmodel;
    public PrincipalPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext=viewModel;
        _viewmodel = viewModel;

	}

    async void  ImageButton_Clicked(System.Object sender, System.EventArgs e)
    {
        // Asegúrate de que el sender sea un ImageButton
        if (sender is ImageButton button)
        {
            // Obtén el contexto de datos del botón, que debe ser DetallePlanificacion
            var item = button.BindingContext as DetallePlanificacion;

            if (item != null)
            {
                // Lógica para compartir
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = $"El día: {item.fechaCorte} se realizarán cortes desde: {item.horaDesde} hasta {item.horaHasta}",
                    Title = "Compartir Item"
                });
            }
        }
    }
    //private void Clicked(object sender, EventArgs e)
    //{
    //    myCheckBox.IsChecked = !myCheckBox.IsChecked;
    //}
}