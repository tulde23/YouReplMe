namespace YouReplMe.Actions
{
    public abstract class AbstractActionWithChildren<T> : AbstractAction<T> where T : RegexVerb
    {
        protected readonly IChildActionActivator<T> childActionActivator;

        protected AbstractActionWithChildren(IHelpTextBuilderFactory helpTextBuilderFactory, IChildActionActivator<T> childActionActivator) : base(helpTextBuilderFactory)
        {
            this.childActionActivator = childActionActivator;
        }

        protected abstract string GetChildKey(T verb);

        protected override async Task RunAsync(T verb, CancellationToken cancellationToken)
        {
            var key = GetChildKey(verb);
            try
            {
                var changeType = this.childActionActivator.Activate(key);

                await changeType.RunAsync(verb, cancellationToken);
            }
            catch
            {
                Console.WriteLine($"Action {key} not found.".Danger());
            }
        }
    }
}