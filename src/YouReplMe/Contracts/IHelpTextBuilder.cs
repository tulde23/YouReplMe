namespace YouReplMe.Contracts
{
    /// <summary>
    /// builds help text for the app
    /// </summary>
    public interface IHelpTextBuilder : ISingletonDependency
    {
        void DisplayCommandNameAndDescription(CommandHelpText commandHelpText);

        void DisplayExamples(IReadOnlyCollection<HelpText> exmaples);

        void DisplayOptions(IReadOnlyCollection<HelpText> options);

        void DisplayParameters(IReadOnlyCollection<HelpText> parameters);

        void DisplayUsage(CommandHelpText commandHelpText);

        void DisplayHelpText(CommandHelpText commandHelpText);
    }
    public interface IHelpTextBuilderFactory : ISingletonDependency
    {
        IHelpTextBuilder GetHelpTextBuilder(FormatType key);
        void DisplayHelpText(CommandHelpText commandHelpText, FormatType key = FormatType.Minimal);
    }
}