using CommunityToolkit.Mvvm.ComponentModel;
using SampleApp.Mobile.Models;
using SampleApp.Shared;

namespace SampleApp.Mobile.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(Message), "Message")]
public partial class MessageDetailPageViewModel
{
    [ObservableProperty] 
    MessageForCSharp message;
}