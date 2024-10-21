using Divisas.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Divisas.Views
{
    public partial class Editar : ContentPage
    {
        private Moneda moneda;

        public Editar(Moneda moneda)
        {
            InitializeComponent();
            this.moneda = moneda;

            // Cargar los datos de la moneda en los campos de la vista
            Clave.Text = moneda.clave;
            PrecioV.Text = moneda.valor_venta.ToString("F2"); // Formato a dos decimales
            PrecioC.Text = moneda.valor_compra.ToString("F2"); // Formato a dos decimales
        }

        private async void OnRegresarClicked(object sender, EventArgs e)
        {
            // Navegar de vuelta a Home
            await Navigation.PopAsync();
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(Clave.Text) || string.IsNullOrWhiteSpace(PrecioV.Text) || string.IsNullOrWhiteSpace(PrecioC.Text))
            {
                await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
                return;
            }

            // Validar la clave (máximo 3 letras)
            if (!Regex.IsMatch(Clave.Text, @"^[A-Za-z]{3}$"))
            {
                await DisplayAlert("Error", "La clave debe tener 3 letras sin números.", "OK");
                return;
            }

            // Convertir la clave a mayúsculas
            string claveMayus = Clave.Text.ToUpper();

            // Intentar convertir los precios a decimales con validación de dos decimales
            if (!decimal.TryParse(PrecioV.Text, out decimal precioVenta) || !decimal.TryParse(PrecioC.Text, out decimal precioCompra))
            {
                await DisplayAlert("Error", "Por favor ingrese precios válidos.", "OK");
                return;
            }

            // Validar que los precios tengan solo 2 decimales
            if (Math.Round(precioVenta, 2) != precioVenta || Math.Round(precioCompra, 2) != precioCompra)
            {
                await DisplayAlert("Error", "Los precios deben tener solo 2 decimales.", "OK");
                return;
            }

            // Validar si la clave de la moneda ya existe en la base de datos
            try
            {
                using (var db = new DivisasDbContext())
                {
                    var monedaExistente = db.Monedas.FirstOrDefault(m => m.clave == claveMayus && m.id != moneda.id);

                    // Si ya existe una moneda con esa clave, mostrar un mensaje de error
                    if (monedaExistente != null)
                    {
                        await DisplayAlert("Error", "Ya existe una moneda con esta clave.", "OK");
                        return;
                    }

                    // Actualizar los valores de la moneda
                    moneda.clave = claveMayus;
                    moneda.valor_venta = precioVenta;
                    moneda.valor_compra = precioCompra;

                    // Guardar los cambios
                    db.Monedas.Update(moneda);
                    await db.SaveChangesAsync(); // Guardar los cambios de manera asíncrona
                }

                await DisplayAlert("Éxito", "Cambios guardados correctamente.", "OK");
                await Navigation.PopAsync(); // Regresar a la vista Home después de guardar
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un problema al guardar la moneda: {ex.Message}", "OK");
            }
        }
    }
}
