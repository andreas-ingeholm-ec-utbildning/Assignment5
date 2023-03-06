using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;

namespace IssuesClient.ViewModels;

public partial class AddReport : ViewModel
{

    public override string Title => "Issue browser - Add report";

    [ObservableProperty, Required] private User m_user = null!;
    [ObservableProperty, Required] private string m_comment = null!;

    [RelayCommand]
    void Add()
    {

        GoBack();
    }

    [RelayCommand]
    void Cancel() =>
        GoBack();

}
