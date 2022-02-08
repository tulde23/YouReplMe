namespace YouReplMe.Contracts
{
    public interface IConfirmableOperation : ITransientDependency
    {
        public ValueTask<bool> ConfirmAsync(Func<bool> condition, Func<Task> operation);
    }
}