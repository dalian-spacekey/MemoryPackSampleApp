using SampleApp.Mobile.ViewModels;

namespace SampleApp.Mobile.Views;

public partial class PersonDetailPage : ContentPage
{
	public PersonDetailPage(PersonDetailPageViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}