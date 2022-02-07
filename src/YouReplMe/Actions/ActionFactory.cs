using Autofac.Features.Indexed;

namespace YouReplMe.Actions
{
    public class ActionFactory : IActionFactory, ITransientDependency
    {
        private readonly IIndex<string, IHelpProvider> helpProviders;
        private readonly IEnumerable<IHelpProvider> allHelpProviders;

        public ActionFactory(
            IIndex<string, IAction> actions,
            IEnumerable<IAction> allActions,
            IIndex<string, IHelpProvider> helpProviders,
            IEnumerable<IHelpProvider> allHelpProviders)
        {
            Actions = actions;
            AllActions = allActions;
            this.helpProviders = helpProviders;
            this.allHelpProviders = allHelpProviders;
        }

        public IIndex<string, IAction> Actions { get; }
        public IEnumerable<IAction> AllActions { get; }

        public IAction GetActionByKey(string key)
        {
            try
            {
                return Actions[key];
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<CommandHelpText> GetAllHelp()
        {
            foreach (var item in allHelpProviders)
            {
                var text = item.GetHelpText();
                yield return text;
            }
        }

        public CommandHelpText GetHelpByKey(string key)
        {
            if (helpProviders.TryGetValue(key, out var result))
            {
                return result.GetHelpText();
            }
            return null;
        }
    }
}