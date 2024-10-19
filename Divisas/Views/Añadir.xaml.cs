using Divisas.Models;
using System.Text;

namespace Divisas;

public partial class Añadir : ContentPage
{
    public Añadir()
    {
        InitializeComponent();
    }

    // Evento click para el botón de guardar
    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        // Validar que los campos no estén vacíos
        if (string.IsNullOrWhiteSpace(Clave.Text) || string.IsNullOrWhiteSpace(PrecioV.Text) || string.IsNullOrWhiteSpace(PrecioC.Text))
        {
            await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
            return;
        }

        // Intentar convertir los precios a decimales
        if (!decimal.TryParse(PrecioV.Text, out decimal precioVenta) || !decimal.TryParse(PrecioC.Text, out decimal precioCompra))
        {
            await DisplayAlert("Error", "Por favor ingrese precios válidos.", "OK");
            return;
        }

        // Crear una nueva instancia de Moneda
        Moneda nuevaMoneda = new Moneda
        {
            clave = Clave.Text,
            valor_venta = precioVenta,
            valor_compra = precioCompra
        };

        // Validar si la clave de la moneda ya existe en la base de datos
        try
        {
            using (var db = new DivisasDbContext())
            {
                var monedaExistente = db.Monedas.FirstOrDefault(m => m.clave == nuevaMoneda.clave);

                // Si ya existe una moneda con esa clave, mostrar un mensaje de error
                if (monedaExistente != null)
                {
                    await DisplayAlert("Error", "Ya existe una moneda con esta clave.", "OK");
                    return;
                }

                // Si no existe, procedemos a guardar la nueva moneda
                db.Monedas.Add(nuevaMoneda);
                await db.SaveChangesAsync(); // Guardar los cambios de manera asíncrona
            }

            await DisplayAlert("Éxito", "Moneda guardada correctamente.", "OK");

            //Mostrar Monedas
            //await MostrarMonedasGuardadas();

            // Opcional: limpiar los campos después de guardar
            Clave.Text = string.Empty;
            PrecioV.Text = string.Empty;
            PrecioC.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al guardar la moneda: {ex.Message}", "OK");
        }
    }

    // Método para mostrar las monedas guardadas
    private async Task MostrarMonedasGuardadas()
    {
        try
        {
            using (var db = new DivisasDbContext())
            {
                var monedas = db.Monedas.ToList();

                if (monedas.Count == 0)
                {
                    await DisplayAlert("Monedas", "No hay monedas guardadas.", "OK");
                    return;
                }

                // Construir el texto con la información de las monedas
                StringBuilder sb = new StringBuilder();
                foreach (var moneda in monedas)
                {
                    sb.AppendLine($"Clave: {moneda.clave}, Compra: {moneda.valor_compra}, Venta: {moneda.valor_venta}");
                }

                // Mostrar en un DisplayAlert
                await DisplayAlert("Monedas Guardadas", sb.ToString(), "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al obtener las monedas: {ex.Message}", "OK");
        }
    }
}
