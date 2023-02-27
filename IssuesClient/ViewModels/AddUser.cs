using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace IssuesClient.ViewModels;

public partial class AddUser : ViewModel
{

    [Required, ObservableProperty] private string m_firstName = null!;
    [Required, ObservableProperty] private string m_lastName = null!;
    [Required, ObservableProperty] private int m_phoneNumber;

    [Required, ObservableProperty]
    [CustomValidation(typeof(AddReport), nameof(ValidateProperty))]
    private string m_emailAddress = null!;

    [Required, ObservableProperty]
    [CustomValidation(typeof(AddReport), nameof(ValidatePassword))]
    private string m_password = null!;

    public static ValidationResult ValidateEmailAddress(string value, ValidationContext context)
    {
        //TODO: Ensure email address is unique
        return ValidationResult.Success!;
    }

    public static ValidationResult ValidatePassword(string value, ValidationContext context)
    {
        //TODO: Ensure password is strong enough
        return ValidationResult.Success!;
    }

    [RelayCommand]
    void Add()
    {

        GoBack();
    }

    [RelayCommand]
    void Cancel() =>
        GoBack();

}
