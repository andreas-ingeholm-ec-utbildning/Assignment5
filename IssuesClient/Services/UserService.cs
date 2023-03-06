using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IssuesClient.Models;
using IssuesClient.Models.Entities;

namespace IssuesClient.Services;

public class UserService : DBService<UserEntity>
{

    //TODO: Rewrite services into static services, with no inheritance again, merge UserProfileService into UserService, its too confusing currently
    //TODO: Delete db and start over again

    public override Task<UserEntity> CreateAsync(UserEntity entity) => throw new NotSupportedException();
    public override Task Update(UserEntity entity) => throw new NotSupportedException();
    public override Task Remove(Expression<Func<UserEntity, bool>> predicate) => throw new NotSupportedException();

    public async Task CreateAsync(User model, string password)
    {

        var profileService = new UserProfileService();

        var entity = (UserEntity)model;
        entity.SetSecurePassword(password);

        _ = await profileService.CreateAsync(new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            User = entity
        });

    }

    public async Task Update(User entity, string password)
    {

        Authenticate(entity, password);

        if (await GetAsync(e => e.Id == entity.Id) is UserEntity existingEntity)
        {
            existingEntity.EmailAddress = entity.EmailAddress;
            existingEntity.SetSecurePassword(password);
            await base.Update(existingEntity);
        }

    }

    public override Task<IEnumerable<UserEntity>> GetAll()
    {
        return base.GetAll();
    }

    public override Task<UserEntity?> GetAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        return base.GetAsync(predicate);
    }

    public async Task Remove(UserEntity entity, string password)
    {
        //Authenticate(entity, password);
        await base.Remove(e => e.Id == entity.Id);
    }

    static void Authenticate(UserEntity entity, string password)
    {
        if (!entity.ValidatePassword(password))
            throw new UnauthorizedAccessException();
    }

}
