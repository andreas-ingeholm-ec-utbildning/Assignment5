using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;
using IssuesClient.Utility;

namespace IssuesClient.ViewModels;

public partial class AddUser : ViewModel
{

    public AddUser()
    {
        ErrorsChanged += AddUser_ErrorsChanged;
        ValidateAllProperties();
    }

    void AddUser_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e) =>
        Errors = GetErrors().Select(v => v.ErrorMessage).Where(s => !string.IsNullOrWhiteSpace(s)).OfType<string>().ToArray();

    [ObservableProperty]
    private string[] m_errors = Array.Empty<string>();

    public override string Title => "Issue browser - Add user";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "First name cannot be empty.")]
    private string m_firstName = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [MinLength(1, ErrorMessage = "First name cannot be empty.")]
    private string m_lastName = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Phone(ErrorMessage = "Phone number is not valid.")]
    private string m_phoneNumber = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [EmailAddress(ErrorMessage = "Email address is not valid.")]
    private string m_emailAddress = null!;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Password(ErrorMessage = "Password must be at least 8 characters in length, contain at least lower case and one uppercast letter, one numeric character, and one special character.")]
    private string m_password = null!;

    [RelayCommand]
    void Add() =>
        DoActionWithLoadingScreen(async () =>
        {
            var service = new UserService();
            await service.CreateAsync(new User() { EmailAddress = EmailAddress, FirstName = FirstName, LastName = LastName, PhoneNumber = PhoneNumber }, Password);
            GoBack();
        });

    [RelayCommand]
    void Cancel() =>
        GoBack();

    [RelayCommand]
    void FillTestData()
    {
        FirstName = "Test" + UserService.CachedListCount;
        LastName = "Testsson";
        PhoneNumber = "0123456789";
        EmailAddress = FirstName + "." + LastName + "@gmail.com";
        Password = "Aa!12345678";
    }

}
