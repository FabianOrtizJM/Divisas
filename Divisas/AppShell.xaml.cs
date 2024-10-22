using Divisas.Models;
using System.ComponentModel;
using System.Linq;

namespace Divisas
{
    public partial class AppShell : Shell, INotifyPropertyChanged
    {
        private Sucursal _sucursal;

        public Sucursal Sucursal
        {
            get => _sucursal;
            set
            {
                if (_sucursal != value)
                {
                    _sucursal = value;
                    OnPropertyChanged(nameof(Sucursal));
                }
            }
        }

        public AppShell()
        {
            InitializeComponent();
            CargarSucursal();
            BindingContext = this; // Establece el contexto de datos
        }

        private void CargarSucursal()
        {
            using (var db = new DivisasDbContext())
            {
                Sucursal = db.Sucursales.FirstOrDefault() ?? new Sucursal
                {
                    nombre_empresa = "Nombre de la Empresa",
                    foto = "ruta_por_defecto.jpg" // Puedes usar una imagen por defecto aquí
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
