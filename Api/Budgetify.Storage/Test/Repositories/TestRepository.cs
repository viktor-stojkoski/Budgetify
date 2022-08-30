namespace Budgetify.Storage.Test.Repositories
{
    using System;
    using System.Threading.Tasks;

    using Budgetify.Common.Results;
    using Budgetify.Storage.Common.Repositories;
    using Budgetify.Storage.Infrastructure.Context;
    using Budgetify.Storage.Test.Entities;

    using Microsoft.EntityFrameworkCore;

    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(IBudgetifyDbContext budgetifyDbContext)
            : base(budgetifyDbContext) { }

        public async Task<Result<Test>> GetTestAsync(Guid testUid)
        {
            Test? dbTest = await AllNoTrackedOf<Test>()
                .SingleOrDefaultAsync(x => x.Uid == testUid);

            if (dbTest is null)
            {
                return Result.NotFound<Test>("ERROR");
            }

            return Result.Ok(dbTest);
        }

        public void Insert(int num)
        {
            //Insert(
            //    new Test
            //    {
            //        Uid = Guid.NewGuid(),
            //        CreatedOn = DateTime.UtcNow,
            //        DeletedOn = null,
            //        Name = $"Test Name {num}",
            //        Address = $"Test Address {num}"
            //    });
        }

        public void Update(Test test)
        {
            AttachOrUpdate(test, EntityState.Modified);
        }
    }

    public interface ITestRepository
    {
        Task<Result<Test>> GetTestAsync(Guid testUid);

        void Insert(int num);

        void Update(Test test);
    }
}
