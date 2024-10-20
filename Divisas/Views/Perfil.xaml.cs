using Divisas.Models;
using System;
using System.IO;
using Microsoft.Maui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Divisas;
public partial class Perfil : ContentPage
{
    public Perfil()
    {
        InitializeComponent();
        VerificarSucursal();
        CargarDatosSucursal();
    }

    private void VerificarSucursal()
    {
        using (var db = new DivisasDbContext())
        {
            // Verificar si hay una sucursal en la base de datos
            if (!db.Sucursales.Any())
            {
                // Si no hay sucursal, crear una nueva
                var sucursal = new Sucursal
                {
                    nombre_empresa = "Mi empresa",
                    direccion = "Calle 123",
                    ciudad = "Ciudad",
                    estado = "Estado",
                    codigo_postal = 12345,
                    foto = "perfil.jpg"
                };

                db.Sucursales.Add(sucursal);
                db.SaveChanges();
            }
        }
    }

    private void CargarDatosSucursal()
    {
        var sucursal = ObtenerSucursal(); // Cargar la sucursal desde la base de datos

        if (sucursal != null)
        {
            // Asignar los datos a los controles
            nombre_empresa.Text = sucursal.nombre_empresa;
            direccion.Text = sucursal.direccion;
            direccion2.Text = sucursal.direccion2;
            codigo_postal.Text = sucursal.codigo_postal.ToString();
            ciudad.Text = sucursal.ciudad;
            estado.Text = sucursal.estado;
            foto.Source = sucursal.foto; // Asignar la imagen
        }
        else
        {
            // Manejar el caso en que no se encuentra la sucursal
            DisplayAlert("Error", "No se encontró la sucursal.", "OK");
        }
    }

    private Sucursal ObtenerSucursal()
    {
        // Asumiendo que tienes un identificador único de la sucursal, como un ID.
        int sucursalId = 1; // Cambia esto por el ID real de la sucursal que estás buscando.

        using (var db = new DivisasDbContext())
        {
            // Busca la sucursal en la base de datos
            return db.Sucursales.FirstOrDefault(s => s.id == sucursalId);
        }
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        // Validar que los campos no estén vacíos
        if (string.IsNullOrEmpty(nombre_empresa.Text) || string.IsNullOrEmpty(direccion.Text) ||
            string.IsNullOrEmpty(ciudad.Text) || string.IsNullOrEmpty(estado.Text) || string.IsNullOrEmpty(codigo_postal.Text))
        {
            await DisplayAlert("Error", "Por favor complete todos los campos.", "OK");
            return;
        }

        // Guardar los datos de la sucursal en la base de datos
        try
        {
            var sucursalActualizada = new Sucursal
            {
                id = 1, // Cambia esto por el ID real de la sucursal que estás actualizando
                nombre_empresa = nombre_empresa.Text,
                direccion = direccion.Text,
                direccion2 = direccion2.Text,
                codigo_postal = int.Parse(codigo_postal.Text),
                ciudad = ciudad.Text,
                estado = estado.Text,
                foto = foto.Source?.ToString()
                // Asegúrate de que la ruta sea correcta
            };

            using (var db = new DivisasDbContext())
            {
                // Lógica para actualizar la sucursal en la base de datos
                db.Sucursales.Update(sucursalActualizada);
                await db.SaveChangesAsync(); // Guardar los cambios
            }

            await DisplayAlert("Éxito", "Sucursal actualizada correctamente.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al actualizar la sucursal: {ex.Message}", "OK");
        }
    }
}