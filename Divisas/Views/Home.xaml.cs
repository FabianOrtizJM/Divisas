using Divisas;
using ViewModels;

namespace Divisas;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
		BindingContext = new HomeViewModel();
    }
}