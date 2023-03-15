using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class AddUser : ViewModel
{

    public override string Title => "Issue browser - Add user";

    #region Properties

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "First name cannot be empty.")]
    [Required]
    private string m_firstName = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "First name cannot be empty.")]
    [Required]
    private string m_lastName = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Phone(ErrorMessage = "Phone number is not valid.")]
    [Required]
    private string m_phoneNumber = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    [Required]
    private string m_emailAddress = null!;

    #endregion
    #region Commands

    [RelayCommand]
    void Add() =>
        DoActionWithLoadingScreen(async () =>
        {
            await UserService.CreateAsync(new User() { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName, PhoneNumber = PhoneNumber });
            GoBack();
        });

    [RelayCommand]
    void Cancel() =>
        GoBack();

    [RelayCommand]
    void FillTestData()
    {
        FirstName = "Test" + UserService.CachedCount;
        LastName = "Testsson";
        PhoneNumber = "0123456789";
        EmailAddress = FirstName + "." + LastName + "@gmail.com";
    }

    #endregion

}
