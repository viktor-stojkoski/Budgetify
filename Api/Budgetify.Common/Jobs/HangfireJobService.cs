namespace Budgetify.Common.Jobs
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Hangfire;

    public class HangfireJobService : IJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public HangfireJobService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public string Enqueue(Expression<Action> methodCall)
        {
            return _backgroundJobClient.Enqueue(methodCall);
        }

        public string Enqueue<T>(Expression<Func<T, Task>> methodCall)
        {
            return _backgroundJobClient.Enqueue(methodCall);
        }

        public string Enqueue<T>(Expression<Action<T>> methodCall)
        {
            return _backgroundJobClient.Enqueue(methodCall);
        }

        public string Enqueue(Expression<Func<Task>> methodCall)
        {
            return _backgroundJobClient.Enqueue(methodCall);
        }
    }
}
