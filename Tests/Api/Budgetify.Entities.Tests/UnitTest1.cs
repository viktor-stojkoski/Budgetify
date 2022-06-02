namespace Budgetify.Entities.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string value = null;

            Budgetify.Common.Results.Result<User.Domain.User> user = User.Domain.User.Create(System.Guid.NewGuid(), DateTime.Now, value, "test");

            Assert.That(user.IsFailureOrNull, Is.True);
        }
    }
}