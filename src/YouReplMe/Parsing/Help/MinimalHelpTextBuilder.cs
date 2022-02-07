namespace YouReplMe.Parsing
{
    /// <summary>
    /// {CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
    /// </summary>
    /// <seealso cref="YouReplMe.Contracts.IHelpTextBuilder" />
    [NamedDependency(Key)]
    internal class MinimalHelpTextBuilder : IHelpTextBuilder
    {
        public const FormatType Key = FormatType.Minimal;

        public void DisplayCommandNameAndDescription(CommandHelpText commandHelpText)
        {
            var commandName = commandHelpText.CommandName.Bold().Light();
            var description = commandHelpText.CommandDescription.Italic().White();
            Console.WriteLine("{0,-55}: {1,-150}", commandName, description);
        }

        public void DisplayExamples(IReadOnlyCollection<HelpText> exampleHelpTextBuilder)
        {
        }

        public void DisplayHelpText(CommandHelpText commandHelpText)
        {
            DisplayCommandNameAndDescription(commandHelpText);
        }

        public void DisplayOptions(IReadOnlyCollection<HelpText> optionsHelpTextBuilder)
        {
        }

        public void DisplayParameters(IReadOnlyCollection<HelpText> parameterHelpTextBuilder)
        {
        }

        public void DisplayUsage(CommandHelpText commandHelpText)
        {
        }
    }
}