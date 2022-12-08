using CommunityToolkit.Mvvm.ComponentModel;
using SampleApp.Mobile.Models;
using SampleApp.Shared;

namespace SampleApp.Mobile.ViewModels;

[INotifyPropertyChanged]
public partial class PersonListPageViewModel
{
    readonly PersonModel personModel;

    [ObservableProperty] 
    PersonForCSharp[] persons;

    [ObservableProperty] 
    PersonForCSharp selectedItem;

    public PersonListPageViewModel(PersonModel personModel)
    {
        this.personModel = personModel;
        LoadPersonsAsync().ConfigureAwait(false);
    }

    async Task LoadPersonsAsync()
    {
        Persons = await personModel.LoadAsync();
    }

    async partial void OnSelectedItemChanged(PersonForCSharp value)
    {
        await Shell.Current.GoToAsync("PersonDetailPage", new Dictionary<string, object>
        {
            { "Person", value }
        });
    }
}