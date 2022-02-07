namespace YouReplMe.Actions
{
    public abstract class AbstractAction<T> : IAction where T : RegexVerb
    {
        private readonly IHelpTextBuilderFactory helpTextBuilderFactory;

        /// <summary>
        /// Gets the name of the action.  this will automatically be added as the first key when call GetRegexBuilder()
        /// builder.SetACtion(this.Action
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public abstract string ActionName { get; }

        protected AbstractAction(IHelpTextBuilderFactory helpTextBuilderFactory)
        {
            this.helpTextBuilderFactory = helpTextBuilderFactory;
        }

        public CommandHelpText GetHelpText()
        {
            var builder = new CommandHelpTextBuilder<T>();
            var helpText = this.GetHelpTextForVerb(builder);
            return helpText;
        }

        public abstract CommandHelpText<T> GetHelpTextForVerb(CommandHelpTextBuilder<T> builder);

        public virtual RegexBuilder<T> GetRegexBuilder()
        {
            var builder = new RegexBuilder<T>();
            builder.SetAction(this.ActionName);
            return BuildRegex(builder);
        }

        protected abstract RegexBuilder<T> BuildRegex(RegexBuilder<T> builder);

        protected abstract Task RunAsync(T verb, CancellationToken cancellationToken);

        public async Task RunAsync(string commandLine, CancellationToken cancellationToken)
        {
            var builder = GetRegexBuilder();
            CommandLineArguments args = builder.ToRegex();
            var verb = args.ToVerb<T>(commandLine);

            if (verb == null)
            {
                helpTextBuilderFactory.DisplayHelpText(GetHelpText(), FormatType.Default);
                return;
            }
            await RunAsync(verb, cancellationToken);
        }
    }
}