using SampleApp.Shared;

namespace SampleApp.Mobile.Models;

public class MessageModel
{
    readonly MemoryPackClient client;

    public MessageModel(MemoryPackClient client)
    {
        this.client = client;
    }

    public async Task<MessageForCSharp[]> LoadAsync() 
        => await client.GetMessagesAsync();
}