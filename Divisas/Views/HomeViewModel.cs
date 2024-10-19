using Divisas;
using Divisas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        // Colección observable para almacenar y reflejar las monedas en la vista.
        public ObservableCollection<Moneda> Monedas { get; set; } = new ObservableCollection<Moneda>();

        // Comando para cargar las monedas, enlazado desde la vista.
        public Command CargarMonedasCommand { get; }

        // Evento para notificar cambios en las propiedades
        public event PropertyChangedEventHandler PropertyChanged;

        public HomeViewModel()
        {
            // Inicializar el comando con el método que carga las monedas.
            CargarMonedasCommand = new Command(async () => await CargarMonedas());

            // Cargar monedas automáticamente al inicializar el ViewModel.
            Task.Run(async () => await CargarMonedas());
        }

        // Método para cargar las monedas desde la base de datos.
        private async Task CargarMonedas()
        {
            try
            {
                // Limpiar la colección de monedas existente antes de cargar nuevas
                Monedas.Clear();

                // Conexión a la base de datos para obtener las monedas.
                using (var db = new DivisasDbContext())
                {
                    var monedas = await db.Monedas.ToListAsync();

                    // Agregar cada moneda a la colección observable.
                    foreach (var moneda in monedas)
                    {
                        Monedas.Add(moneda);
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error, podría usarse algún mecanismo de registro o alertas.
                Console.WriteLine($"Error al cargar las monedas: {ex.Message}");
            }
        }

        // Método para notificar cambios en las propiedades.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
