using Divisas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Divisas.ViewModels
{
    public class CompraVentaViewModel : INotifyPropertyChanged
    {
        private DivisasDbContext _dbContext;
        public ObservableCollection<Moneda> Monedas { get; set; }
        private Moneda _selectedMoneda;
        private string _monto;
        private string _resultado;
        private bool _esCompra;  // Nueva propiedad para determinar si es compra o venta

        public Moneda SelectedMoneda
        {
            get => _selectedMoneda;
            set
            {
                _selectedMoneda = value;
                OnPropertyChanged();
                RealizarOperacion();
            }
        }

        public string Monto
        {
            get => _monto;
            set
            {
                _monto = value;
                OnPropertyChanged();
                RealizarOperacion();
            }
        }

        public string Resultado
        {
            get => _resultado;
            set
            {
                _resultado = value;
                OnPropertyChanged();
            }
        }

        // Propiedad para determinar si es una compra o venta
        public bool EsCompra
        {
            get => _esCompra;
            set
            {
                _esCompra = value;
                OnPropertyChanged();
                RealizarOperacion(); // Actualiza el resultado cuando cambia entre compra y venta
            }
        }

        // Constructor
        public CompraVentaViewModel()
        {
            Console.WriteLine("Constructor de CompraVentaViewModel llamado");

            _dbContext = new DivisasDbContext();
            _dbContext.EnsureCreated();
            _dbContext.SeedData(); // Inserta datos de prueba
            Monedas = new ObservableCollection<Moneda>();
            EsCompra = false; // Por defecto, seleccionamos "vender"
            _monto = string.Empty;
            _resultado = string.Empty;

            // Cargar las monedas de la base de datos
            _ = LoadMonedas(); // Llama al método async sin esperar en el constructor
        }

        // Método para cargar monedas desde la base de datos
        public async Task LoadMonedas()
        {
            try
            {
                var monedas = await _dbContext.Monedas.ToListAsync();
                Monedas.Clear();
                foreach (var moneda in monedas)
                {
                    Monedas.Add(moneda);
                }

                if (Monedas.Any())
                {
                    SelectedMoneda = Monedas.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                // Puedes usar un Log o un Alert para ver si hay errores
                Console.WriteLine($"Error cargando monedas: {ex.Message}");
            
            }
        }
        // Lógica para realizar la conversión de divisas
        public void RealizarOperacion()
        {
            // Usar decimal para manejar cantidades con decimales
            if (SelectedMoneda != null && !string.IsNullOrEmpty(Monto) && decimal.TryParse(Monto, out decimal montoValue))
            {
                decimal valorOperacion = EsCompra ? (decimal)SelectedMoneda.valor_compra : (decimal)SelectedMoneda.valor_venta;
                Resultado = EsCompra
                    ? $"La compra está a un total de ${(montoValue * valorOperacion):F2}"
                    : $"La venta está a un total de ${(montoValue * valorOperacion):F2} + IVA";
            }
            else
            {
                Resultado = EsCompra
                   ? $"La compra está a un total de ${0}"
                    : $"La venta está a un total de ${0} + IVA";
            }
        }

        // Método para notificar cambios a la UI
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
