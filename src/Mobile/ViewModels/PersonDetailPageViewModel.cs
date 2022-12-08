using CommunityToolkit.Mvvm.ComponentModel;
using SampleApp.Mobile.Models;
using SampleApp.Shared;

namespace SampleApp.Mobile.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(Person), "Person")]
public partial class PersonDetailPageViewModel
{
    [ObservableProperty] 
    PersonForCSharp person;
}