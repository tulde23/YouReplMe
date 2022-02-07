using Autofac.Features.Indexed;

namespace YouReplMe.Parsing.Help
{
    internal class HelpTextBuilderFactory : IHelpTextBuilderFactory
    {
        private readonly IIndex<FormatType, IHelpTextBuilder> index;

        public HelpTextBuilderFactory(IIndex<FormatType, IHelpTextBuilder> index)
        {
            this.index = index;
        }

        public void DisplayHelpText(CommandHelpText commandHelpText, FormatType key = FormatType.Minimal)
        {
            GetHelpTextBuilder(key).DisplayHelpText(commandHelpText);
        }

        public IHelpTextBuilder GetHelpTextBuilder(FormatType key)
        {
            try
            {
                return index[key];
            }
            catch
            {
                return index[DefaultHelpTextBuilder.Key];
            }
        }
    }
}