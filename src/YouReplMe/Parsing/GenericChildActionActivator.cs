using Autofac.Features.Indexed;

namespace YouReplMe.Parsing
{
    internal class GenericChildActionActivator<TVerb> : IChildActionActivator<TVerb> where TVerb : RegexVerb
    {
        private readonly IIndex<string, IChildAction<TVerb>> childActions;

        public GenericChildActionActivator(IIndex<string, IChildAction<TVerb>> childActions)
        {
            this.childActions = childActions;
        }

        public IChildAction<TVerb> Activate(string key)
        {
            return childActions[key];
        }
    }
}