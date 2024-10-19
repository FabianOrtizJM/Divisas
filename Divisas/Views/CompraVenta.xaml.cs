using Divisas.ViewModels;

namespace Divisas;

public partial class CompraVenta : ContentPage
{
    public CompraVenta()
    {
        InitializeComponent();
     
            BindingContext = new CompraVentaViewModel();
        
        // Seleccionar por defecto la opci�n de Vender
        SetVenderActive();
    }

    // M�todo para seleccionar la opci�n de Comprar
    private void OnComprarClicked(object sender, EventArgs e)
    {
        SetComprarActive();
    }

    // M�todo para seleccionar la opci�n de Vender
    private void OnVenderClicked(object sender, EventArgs e)
    {
        SetVenderActive();
    }

    // L�gica para activar visualmente la pesta�a de Comprar
    private void SetComprarActive()
    {
        lineaComprar.Color = Color.FromArgb("#114676");
        lineaVender.Color = Colors.Gray;
        btnComprar.TextColor = Color.FromArgb("#114676");
        btnVender.TextColor = Colors.Gray;

        // Cambia la propiedad del ViewModel
        ((CompraVentaViewModel)BindingContext).EsCompra = true;
    }

    // L�gica para activar visualmente la pesta�a de Vender
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
