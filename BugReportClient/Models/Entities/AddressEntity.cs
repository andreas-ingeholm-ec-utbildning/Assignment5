namespace BugReportClient.Models.Entities;

public class AddressEntity
{

    public int Id { get; set; }
    public string StreetName { get; set; } = string.Empty;
    public int? StreetNumber { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

}