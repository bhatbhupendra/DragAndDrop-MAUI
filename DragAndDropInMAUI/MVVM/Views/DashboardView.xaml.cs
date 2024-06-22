using DragAndDropInMAUI.MVVM.ViewModels;

namespace DragAndDropInMAUI.MVVM.Views;

public partial class DashboardView : ContentPage
{
	public DashboardView()
	{
		InitializeComponent();
		this.BindingContext = new DashboardViewModel();
	}
}