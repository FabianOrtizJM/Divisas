using Divisas.Models;
using System.Text;

namespace Divisas
{
    public partial class Añadir : ContentPage
    {
        public Añadir()
        {
            InitializeComponent();
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
            if (Clave.Text.Length > 3)
            {
                await DisplayAlert("Error", "La clave debe tener un máximo de 3 letras.", "OK");
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
                    var monedaExistente = db.Monedas.FirstOrDefault(m => m.clave == claveMayus);

                    // Si ya existe una moneda con esa clave, mostrar un mensaje de error
                    if (monedaExistente != null)
                    {
                        await DisplayAlert("Error", "Ya existe una moneda con esta clave.", "OK");
                        return;
                    }

                    // Crear un nuevo objeto de moneda y guardarlo
                    var nuevaMoneda = new Moneda
                    {
                        clave = claveMayus,
                        valor_venta = precioVenta,
                        valor_compra = precioCompra
                    };

                    db.Monedas.Add(nuevaMoneda);
                    await db.SaveChangesAsync(); // Guardar los cambios de manera asíncrona
                }
                Clave.Text = string.Empty;
                PrecioV.Text = string.Empty;
                PrecioC.Text = string.Empty;
                await DisplayAlert("Éxito", "Moneda añadida correctamente.", "OK");
                //await Navigation.PopAsync(); // Regresar a la vista anterior después de añadir
                //await Navigation.PushAsync(new Home());
                Shell.Current.GoToAsync("//Home");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un problema al añadir la moneda: {ex.Message}", "OK");
            }
        }
    }
}