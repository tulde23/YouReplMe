using Autofac.Features.Indexed;

namespace YouReplMe.Contracts
{
    public interface IHelpFactory : ISingletonDependency
    {
        IHelpAction CreateHelpAction(string key);
    }

    internal class HelpFactory : IHelpFactory
    {
        private readonly IIndex<string, IHelpAction> index;

        public HelpFactory(IIndex<string, IHelpAction> index)
        {
            this.index = index;
        }

        public IHelpAction CreateHelpAction(string key)
        {
            try
            {
                return index[key];
            }
            catch
            {
                return null;
            }
        }
    }
}

