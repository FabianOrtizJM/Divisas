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
        public ObservableCollection<Moneda> Monedas { get; set; } = new ObservableCollection<Moneda>();
            public Command CargarMonedasCommand { get; }
        private Moneda _selectedMoneda;
        private string _monto;
        private string _resultado;
        private bool _esCompra;  // Nueva propiedad para determinar si es compra o venta
        public event PropertyChangedEventHandler PropertyChanged;

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
            _dbContext = new DivisasDbContext();
            _dbContext.Database.EnsureCreated();
            //_dbContext.SeedData(); // Inserta datos de prueba
            Monedas = new ObservableCollection<Moneda>();
            EsCompra = false; // Por defecto, seleccionamos "vender"
            _monto = string.Empty;
            _resultado = string.Empty;

            // Cargar las monedas de la base de datos
            _ = LoadMonedas(); // Llama al método async sin esperar en el constructor

            CargarMonedasCommand = new Command(async () => await LoadMonedas());

        }

        // Método para cargar monedas desde la base de datos
        /*  public async Task LoadMonedas()
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
                  // Manejo de errores
                  Console.WriteLine($"Error cargando monedas: {ex.Message}");
              }
          }*/

        public async Task LoadMonedas()
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
                    if (Monedas.Any())
                    {
                        SelectedMoneda = Monedas.First();
                    }
                }
            }
            catch (Exception ex)
            {
                // En caso de error, podría usarse algún mecanismo de registro o alertas.
                Console.WriteLine($"Error al cargar las monedas: {ex.Message}");
            }
        }
        public async Task ActualizarMonedas()
        {
            await  LoadMonedas();
        }
        

        // Lógica para realizar la conversión de divisas
        public void RealizarOperacion()
        {
            if (SelectedMoneda != null && !string.IsNullOrEmpty(Monto) && decimal.TryParse(Monto, out decimal montoValue))
            {
                // Asegúrate de que el valor de la moneda no sea nulo
                decimal valorOperacion = EsCompra ? (decimal)SelectedMoneda.valor_compra : (decimal)SelectedMoneda.valor_venta;

                if (valorOperacion > 0)
                {
                    if (EsCompra)
                    {
                        // Para compra: se divide el monto en MXN entre el valor de compra de la moneda seleccionada
                        Resultado = $"La compra está a un total de ${(montoValue / valorOperacion):F2} {SelectedMoneda.clave}";
                    }
                    else
                    {
                        // Para venta: se multiplica el monto en la moneda seleccionada por el valor de venta
                        Resultado = $"La venta está a un total de ${(montoValue * valorOperacion):F2} MXN";
                    }
                }
                else
                {
                    Resultado = "Error: el valor de la operación es inválido.";
                }
            }
            else
            {
                Resultado = EsCompra
                   ? $"La compra está a un total de $0"
                   : $"La venta está a un total de $0";
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
