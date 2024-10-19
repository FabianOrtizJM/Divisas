using Divisas;
using ViewModels;

namespace Divisas;

public partial class Home : ContentPage
{
    private HomeViewModel viewModel;

    public Home()
    {
        InitializeComponent();
        viewModel = new HomeViewModel();
        BindingContext = viewModel;
    }

    // Sobrescribir el método OnAppearing para ejecutar la carga de monedas al entrar en la vista.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Ejecutar el comando para cargar las monedas.
        viewModel.CargarMonedasCommand.Execute(null);
    }
}