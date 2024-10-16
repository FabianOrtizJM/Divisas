namespace Divisas;

public partial class CompraVenta : ContentPage
{
    private bool esCompra = true;

    public CompraVenta()
    {
        InitializeComponent();

        // Inicializar las monedas en el Picker
        List<string> monedas = new List<string> { "USD", "MXN" };
        myPicker.ItemsSource = monedas;
        myPicker.SelectedIndex = 0;

        // Evento para cambiar el resultado al cambiar el monto
        EntryMonto.TextChanged += OnEntryMontoTextChanged;

        // Texto inicial del label de resultado
        ResultadoLabel.Text = "La compra est� a un total de $0 + IVA";

        // Establecer estilos iniciales para los botones
        ActualizarEstilosBotones();
    }

    // Cuando el texto en el Entry cambia, actualizamos el resultado
    private void OnEntryMontoTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryMontoTextChanged(sender, e); // Llamar al m�todo que gestiona el texto
        ActualizarResultado();
    }

    // Evento al presionar el bot�n de Comprar
    private void OnComprarClicked(object sender, EventArgs e)
    {
        esCompra = true; // Indica que es una compra
        ActualizarEstilosBotones();
        ActualizarResultado();
    }

    // Evento al presionar el bot�n de Vender
    private void OnVenderClicked(object sender, EventArgs e)
    {
        esCompra = false; // Indica que es una venta
        ActualizarEstilosBotones();
        ActualizarResultado();
    }

    // M�todo para actualizar el resultado basado en el monto y si es compra o venta
    private void ActualizarResultado()
    {
        if (double.TryParse(EntryMonto.Text, out double monto))
        {
            // Calcular el resultado dependiendo de si es compra o venta
            double resultado = esCompra ? monto * 1.8 : monto * 1.5;

            // Actualizar el texto del label con el resultado
            if (esCompra)
            {
                ResultadoLabel.Text = $"La compra est� a un total de {resultado:C} + IVA";
            }
            else
            {
                ResultadoLabel.Text = $"La venta est� a un total de {resultado:C} + IVA";
            }
        }
        else
        {
            // Si no hay un monto v�lido, mostrar el texto predeterminado
            ResultadoLabel.Text = esCompra
                ? "La compra est� a un total de $0 + IVA"
                : "La venta est� a un total de $0 + IVA";
        }
    }

    // M�todo para actualizar los estilos de los botones
    // M�todo para actualizar los estilos de los botones
    private void ActualizarEstilosBotones()
    {
        if (esCompra)
        {
            btnComprar.BackgroundColor = Colors.White;
            btnComprar.TextColor = Colors.Black;
            lineaComprar.Color = Colors.Black;  // Mostrar la l�nea debajo de Comprar

            btnVender.BackgroundColor = Colors.White;
            btnVender.TextColor = Colors.Gray;
            lineaVender.Color = Colors.Transparent;  // Ocultar la l�nea debajo de Vender
        }
        else
        {
            btnVender.BackgroundColor = Colors.White;
            btnVender.TextColor = Colors.Black;
            lineaVender.Color = Colors.Black;  // Mostrar la l�nea debajo de Vender

            btnComprar.BackgroundColor = Colors.White;
            btnComprar.TextColor = Colors.Gray;
            lineaComprar.Color = Colors.Transparent;  // Ocultar la l�nea debajo de Comprar
        }
    }


    // M�todo para limitar la entrada a n�meros y hasta 10 d�gitos  
    private void EntryMontoTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        string currentText = e.NewTextValue;

        // Solo permitir valores num�ricos  
        if (!string.IsNullOrEmpty(currentText) && !currentText.All(char.IsDigit))
        {
            if (entry != null)
            {
                entry.Text = e.OldTextValue; // Revertir al valor anterior si no es num�rico  
            }
            return;
        }

        // Limitar a 10 d�gitos  
        if (currentText.Length > 10)
        {
            if (entry != null)
            {
                entry.Text = currentText.Substring(0, 10); // Limitar a 10 caracteres  
            }
        }
    }
}