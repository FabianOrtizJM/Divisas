using Divisas.ViewModels;

namespace Divisas;

public partial class CompraVenta : ContentPage
{
    public CompraVenta()
    {
        InitializeComponent();
     
            BindingContext = new CompraVentaViewModel();
        
        // Seleccionar por defecto la opción de Vender
        SetVenderActive();
    }

    // Método para seleccionar la opción de Comprar
    private void OnComprarClicked(object sender, EventArgs e)
    {
        SetComprarActive();
    }

    // Método para seleccionar la opción de Vender
    private void OnVenderClicked(object sender, EventArgs e)
    {
        SetVenderActive();
    }

    // Lógica para activar visualmente la pestaña de Comprar
    private void SetComprarActive()
    {
        lineaComprar.Color = Color.FromArgb("#114676");
        lineaVender.Color = Colors.Gray;
        btnComprar.TextColor = Color.FromArgb("#114676");
        btnVender.TextColor = Colors.Gray;

        // Cambia la propiedad del ViewModel
        ((CompraVentaViewModel)BindingContext).EsCompra = true;
    }

    // Lógica para activar visualmente la pestaña de Vender
    private void SetVenderActive()
    {
        lineaComprar.Color = Colors.Gray;
        lineaVender.Color = Color.FromArgb("#114676");
        btnComprar.TextColor = Colors.Gray;
        btnVender.TextColor = Color.FromArgb("#114676");

        // Cambia la propiedad del ViewModel
        ((CompraVentaViewModel)BindingContext).EsCompra = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Actualiza las monedas cada vez que aparece la pantalla
        await ((CompraVentaViewModel)BindingContext).LoadMonedas();
    }

}
