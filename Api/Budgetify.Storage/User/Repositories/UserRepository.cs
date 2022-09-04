namespace Budgetify.Storage.User.Repositories;

using System;
using System.Threading.Tasks;

using Budgetify.Common.Results;
using Budgetify.Contracts.User.Repositories;
using Budgetify.Entities.User.Domain;
using Budgetify.Storage.Common.Extensions;
using Budgetify.Storage.Common.Repositories;
using Budgetify.Storage.Infrastructure.Context;
using Budgetify.Storage.User.Factories;

using Microsoft.EntityFrameworkCore;

public class UserRepository : Repository<Entities.User>, IUserRepository
{
    public UserRepository(IBudgetifyDbContext budgetifyDbContext)
        : base(budgetifyDbContext) { }

    public void Insert(User user)
    {
        Entities.User dbUser = user.CreateUser();

        Insert(dbUser);
    }

    public void Update(User user)
    {
        Entities.User dbUser = user.CreateUser();

        AttachOrUpdate(dbUser, user.State.GetState());
    }

    public async Task<Result<User>> GetUserAsync(Guid userUid)
    {
        Entities.User? dbUser = await AllNoTrackedOf<Entities.User>()
            .SingleOrDefaultAsync(x => x.Uid == userUid);

        if (dbUser is null)
        {
            return Result.NotFound<User>(ResultCodes.UserNotFound);
        }

        return dbUser.CreateUser();
    }

    public async Task<bool> DoesUserWithEmailExists(string? email)
    {
        return await AllNoTrackedOf<Entities.User>()
            .AnyAsync(x => x.Email == email);
    }
}
