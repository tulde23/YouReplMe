namespace YouReplMe.Actions.Help
{
    [NamedDependency(HelpAction.Key)]
    public class HelpAction : IHelpAction, ITransientDependency
    {
        public const string Key = "help";
        private readonly IActionFactory actionFactory;

        public HelpAction(IActionFactory actionFactory)
        {
            this.actionFactory = actionFactory;
        }

        public IEnumerable<CommandHelpText> HelpTextAsync(string commandLine)
        {
            var tokens = commandLine.Split(" ".ToCharArray());
            //should have two items;
            if (tokens.Length == 2)
            {
                return new List<CommandHelpText>() { actionFactory.GetHelpByKey(tokens[1]) };
            }
            else
            {
                try
                {
                    var help = actionFactory.GetHelpByKey(commandLine);
                    if (help == null)
                    {
                        return actionFactory.GetAllHelp();
                    }
                    return new List<CommandHelpText>() { help };
                }
                catch
                {
                    return actionFactory.GetAllHelp();
                }
            }
        }
    }
}