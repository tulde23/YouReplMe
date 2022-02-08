namespace YouReplMe.Contracts
{
    public interface IChildAction<TVerb> : ITransientDependency where TVerb : RegexVerb
    {
        Task RunAsync(TVerb verb, CancellationToken cancellationToken);
    }
}