namespace YouReplMe.Actions.Help
{
    [NamedDependency(ListAction.Key)]
    public class ListAction : IHelpAction, ITransientDependency
    {
        public const string Key = "list";
        private readonly IActionFactory actionFactory;

        public ListAction(IActionFactory actionFactory)
        {
            this.actionFactory = actionFactory;
        }

        public IEnumerable<CommandHelpText> HelpTextAsync(string commandLine)
        {
            return actionFactory.GetAllHelp();
        }
    }
}