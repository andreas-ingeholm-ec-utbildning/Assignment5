using BugReportClient.Models;
using BugReportClient.Services;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels.Popups;

public partial class AddUserViewModel : PopupViewModel
{

    public User User { get; } = new();

    [RelayCommand]
    public async void Save()
    {
        await UserService.SaveAsync(User);
        Close();
    }

    [RelayCommand]
    public void FillTestData()
    {
        User.FirstName = "Test" + UserService.CachedListCount;
        User.LastName = "Testsson";
        User.EmailAddress = $"test{UserService.CachedListCount}.testsson@gmail.com";
        User.StreetName = "Testå";
        User.StreetNumber = UserService.CachedListCount;
        User.PostalCode = "111111";
        User.City = "Teststad";
    }

}
