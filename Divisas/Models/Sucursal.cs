using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Divisas.Models
{
    public class Sucursal : INotifyPropertyChanged
    {
        private string _nombre_empresa;
        private string _foto;

        public int id { get; set; }

        public string nombre_empresa
        {
            get => _nombre_empresa;
            set
            {
                _nombre_empresa = value;
                OnPropertyChanged(nameof(nombre_empresa));
            }
        }

        public string foto
        {
            get => _foto;
            set
            {
                _foto = value;
                OnPropertyChanged(nameof(foto));
            }
        }

        public string direccion { get; set; }
        public string direccion2 { get; set; }
        public int codigo_postal { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
