namespace BugReportClient.Models.Entities;

public class UserEntity
{

    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public int AddressId { get; set; }

}
