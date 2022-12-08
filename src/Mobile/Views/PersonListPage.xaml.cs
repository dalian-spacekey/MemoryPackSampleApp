using SampleApp.Mobile.ViewModels;

namespace SampleApp.Mobile.Views;

public partial class PersonListPage : ContentPage
{
	public PersonListPage(PersonListPageViewModel viewModel)
    {
        BindingContext = viewModel;
		InitializeComponent();
    }
}