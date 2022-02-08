namespace YouReplMe.Services
{
    internal class ConfirmableOperation : IConfirmableOperation
    {
        public async ValueTask<bool> ConfirmAsync(Func<bool> condition, Func<Task> operation)
        {
            if (condition?.Invoke() == true)
            {
                await operation();
                return true;
            }
            return false;
        }
    }
}