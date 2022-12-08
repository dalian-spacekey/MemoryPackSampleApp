using CommunityToolkit.Mvvm.ComponentModel;
using SampleApp.Mobile.Models;
using SampleApp.Shared;

namespace SampleApp.Mobile.ViewModels;

[INotifyPropertyChanged]
public partial class MessageListPageViewModel
{
    readonly MessageModel messageModel;

    [ObservableProperty] 
    MessageForCSharp[] messages;

    [ObservableProperty] 
    MessageForCSharp selectedItem;

    public MessageListPageViewModel(MessageModel messageModel)
    {
        this.messageModel = messageModel;
        LoadMessagesAsync().ConfigureAwait(false);
    }

    async Task LoadMessagesAsync()
    {
        Messages = await messageModel.LoadAsync();
    }

    async partial void OnSelectedItemChanged(MessageForCSharp value)
    {
        await Shell.Current.GoToAsync("MessageDetailPage", new Dictionary<string, object>
        {
            { "Message", value }
        });
    }
}