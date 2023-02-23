using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugReportClient.Models;
using BugReportClient.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static BugReportClient.Services.DBService;

namespace BugReportClient.Services;

public static class AddressService
{

    public static async Task<IEnumerable<Address>> GetAllAsync() =>
        (await Context.Addresses.ToArrayAsync()).Select(ToModel);

    public static async Task<AddressEntity?> GetEntityAsync(this User user) =>
           await Context.Addresses.FirstOrDefaultAsync(a => a.StreetName == user.StreetName && a.StreetNumber == user.StreetNumber && a.City == user.City && a.PostalCode == user.PostalCode);

    public static Address ToModel(this AddressEntity entity) =>
          new() { Id = entity.Id, StreetName = entity.StreetName, StreetNumber = entity.StreetNumber, City = entity.City, PostalCode = entity.PostalCode };

    public static AddressEntity ToAddressEntity(this User model) =>
        new() { StreetName = model.StreetName, StreetNumber = model.StreetNumber, City = model.City, PostalCode = model.PostalCode };

}
