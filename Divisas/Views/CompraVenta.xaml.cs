namespace Divisas;

public partial class CompraVenta : ContentPage
{
    private bool esCompra = true;
    public CompraVenta()
	{
		InitializeComponent();
        InitializeComponent();

        // Inicializar las monedas en el Picker
        List<string> monedas = new List<string> { "USD", "MXN" };
        myPicker.ItemsSource = monedas;
        myPicker.SelectedIndex = 0;
        myPicker.BackgroundColor = Colors.LightGrey;

        // Evento para cambiar el resultado al cambiar el monto
        EntryMonto.TextChanged += OnEntryMontoTextChanged;


        // Texto inicial del label de resultado
        ResultadoLabel.Text = "La compra está a un total de $0 + IVA";

        // Establecer estilos iniciales para los botones
        btnComprar.BackgroundColor = Color.FromArgb("#114676");
        btnComprar.TextColor = Colors.White;
        btnVender.BackgroundColor = Colors.Transparent;
        btnVender.TextColor = Colors.Gray;
    }
    // Cuando el texto en el Entry cambia, actualizamos el resultado
    private void OnEntryMontoTextChanged(object sender, TextChangedEventArgs e)
    {
        EntryMontoTextChanged(sender, e); // Llamar al método que gestiona el texto
        ActualizarResultado();
    }

    // Evento al presionar el botón de Comprar
    private void OnComprarClicked(object sender, EventArgs e)
    {
        esCompra = true; // Indica que es una compra

        // Cambiar el estilo al presionar "Comprar"
        btnComprar.BackgroundColor = Color.FromArgb("#114676");
        btnComprar.TextColor = Colors.White;

        btnVender.BackgroundColor = Colors.Transparent;
        btnVender.TextColor = Colors.Gray;

        ActualizarResultado();
    }

    // Evento al presionar el botón de Vender
    private void OnVenderClicked(object sender, EventArgs e)
    {
        esCompra = false; // Indica que es una venta

        // Cambiar el estilo al presionar "Vender"
        btnVender.BackgroundColor = Color.FromArgb("#114676");
        btnVender.TextColor = Colors.White;

        btnComprar.BackgroundColor = Colors.Transparent;
        btnComprar.TextColor = Colors.Gray;

        ActualizarResultado();
    }

    // Método para actualizar el resultado basado en el monto y si es compra o venta
    private void ActualizarResultado()
    {
        if (double.TryParse(EntryMonto.Text, out double monto))
        {
            // Calcular el resultado dependiendo de si es compra o venta
            double resultado = esCompra ? monto * 1.8 : monto * 1.5;

            // Actualizar el texto del label con el resultado
            if (esCompra)
            {
                ResultadoLabel.Text = $"La compra está a un total de {resultado:C} + IVA";
            }
            else
            {
                ResultadoLabel.Text = $"La venta está a un total de {resultado:C} + IVA";
            }
        }
        else
        {
            // Si no hay un monto válido, mostrar el texto predeterminado
            ResultadoLabel.Text = esCompra
                ? "La compra está a un total de $0 + IVA"
                : "La venta está a un total de $0 + IVA";
        }
    }

    // Método para limitar la entrada a números y hasta 10 dígitos
    private void EntryMontoTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        string currentText = e.NewTextValue;

        // Solo permitir valores numéricos
        if (!string.IsNullOrEmpty(currentText) && !currentText.All(char.IsDigit))
        {
            entry.Text = e.OldTextValue; // Revertir al valor anterior si no es numérico
            return;
        }

        // Limitar a 10 dígitos
        if (currentText.Length > 10)
        {
            entry.Text = currentText.Substring(0, 10); // Limitar a 10 caracteres
        }
    }
}

