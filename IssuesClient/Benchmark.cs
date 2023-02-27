using System.Linq;
using BenchmarkDotNet.Attributes;
using IssuesClient.Models;
using IssuesClient.Models.Entities;

namespace IssuesClient;

public class Benchmark
{

    [Params(1, 10)]
    public int InstanceCount { get; set; }

    private UserEntity[] directMapping_users = null!;

    [GlobalSetup]
    public void Initialize()
    {
        directMapping_users =
            Enumerable.Range(1, InstanceCount).
            Select((u, i) => new UserEntity()
            {
                FirstName = "test" + i,
                LastName = "testsson",
                EmailAddress = $"test{i}.testsson@gmail.com",
                PhoneNumber = 1234567890,
            }).
            ToArray();
    }

    [Benchmark]
    public void DirectMapping()
    {

        var users =
            directMapping_users.
            Select(entity =>
                new User()
                {
                    Id = entity.Id,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    EmailAddress = entity.EmailAddress,
                    PhoneNumber = entity.PhoneNumber
                }).
            ToArray();

    }

}