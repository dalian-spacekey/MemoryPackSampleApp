using SampleApp.Shared;

namespace SampleApp.Mobile.Models;

public class PersonModel
{
    readonly MemoryPackClient client;

    public PersonModel(MemoryPackClient client)
    {
        this.client = client;
    }

    public async Task<PersonForCSharp[]> LoadAsync()
        => await client.GetPersonsAsync();
}