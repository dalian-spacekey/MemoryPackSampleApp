using SampleApp.Mobile.ViewModels;

namespace SampleApp.Mobile.Views;

public partial class MessageDetailPage : ContentPage
{
	public MessageDetailPage(MessageDetailPageViewModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();
	}
}