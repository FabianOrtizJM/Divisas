using Divisas;
using Divisas.Models;
using Divisas.Views;
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

    // Método para navegar a la vista de editar pasando los datos
    private async void EditarMoneda(Moneda moneda)
    {
        await Navigation.PushAsync(new Editar(moneda));
    }

    //private async void Ejecutar(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    var item = button?.CommandParameter as Moneda;
    //    if (item != null)
    //    {
    //        await DisplayAlert("Detalles del Ítem",
    //            $"Clave: {item.clave}\nCompra: {item.valor_compra}\nVenta: {item.valor_venta}",
    //            "OK");
    //    }
    //    else
    //    {
    //        await DisplayAlert("Error", "No se pudo obtener los datos del ítem.", "OK");
    //    }
    //}

}