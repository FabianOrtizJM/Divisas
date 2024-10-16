using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisas
{
    public static class ConexionDB
    {
        public static string DevolverRuta(string divisasdb)
        {
            string rutaBaseDatos = string.Empty;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                rutaBaseDatos = Path.Combine(rutaBaseDatos, divisasdb);
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                rutaBaseDatos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "..", "Library", divisasdb);
            }

            return rutaBaseDatos;
        }

        public static void BorrarBaseDeDatos(string divisasdb)
        {
            string rutaBaseDatos = DevolverRuta(divisasdb);

            if (File.Exists(rutaBaseDatos))
            {
                File.Delete(rutaBaseDatos);
                Console.WriteLine("Base de datos eliminada.");
            }
            else
            {
                Console.WriteLine("No se encontró la base de datos para eliminar.");
            }
        }
    }
}
