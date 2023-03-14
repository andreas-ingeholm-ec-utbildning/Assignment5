using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

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

}
