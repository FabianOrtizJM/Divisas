using Divisas.Models;

namespace Divisas.Views;

public partial class Editar : ContentPage
{
    private Moneda moneda;

    public Editar(Moneda moneda)
    {
        InitializeComponent();
        this.moneda = moneda;

        // Cargar los datos de la moneda en los campos de la vista
        Clave.Text = moneda.clave;
        PrecioV.Text = moneda.valor_venta.ToString();
        PrecioC.Text = moneda.valor_compra.ToString();
    }

    private async void OnRegresarClicked(object sender, EventArgs e)
    {
        // Navegar de vuelta a Home
        await Navigation.PopAsync();
    }
}
