using SampleApp.Mobile.ViewModels;

namespace SampleApp.Mobile.Views;

public partial class MessageListPage : ContentPage
{
	public MessageListPage(MessageListPageViewModel viewModel)
    {
        BindingContext = viewModel;
		InitializeComponent();
    }
}