namespace YouReplMe.Parsing
{
    [NamedDependency(Key)]
    internal class DefaultHelpTextBuilder : IHelpTextBuilder
    {
        public const FormatType Key = FormatType.Default;

        public void DisplayCommandNameAndDescription(CommandHelpText commandHelpText)
        {
            var commandName = commandHelpText.CommandName.Bold().Light();
            var description = commandHelpText.CommandDescription.Italic().White();
            Console.WriteLine($"{commandName} {description}");
        }

        public void DisplayExamples(IReadOnlyCollection<HelpText> examples)
        {
            if (examples?.Any() == true)
            {
                Display($"{"EXAMPLES:".Bold().Info()}", 1);
                foreach (var example in examples.ToList())
                {
                    Display($"{" ".Generate(5)}{example.Text.Success()}", 1);
                    if (!string.IsNullOrEmpty(example.Description))
                    {
                        Display($"{" ".Generate(5)}{example.Description.Secondary().Italic()}", 1);
                    }
                    Console.WriteLine();
                }
            }
        }

        public void DisplayHelpText(CommandHelpText commandHelpText)
        {
            DisplayCommandNameAndDescription(commandHelpText);
            DisplayUsage(commandHelpText);
            DisplayParameters(commandHelpText.Parameters);
            DisplayOptions(commandHelpText.Options);
            DisplayExamples(commandHelpText.Examples);
            Console.WriteLine("");
        }

        public void DisplayOptions(IReadOnlyCollection<HelpText> options)
        {
            if (options?.Any() == true)
            {
                Display($"OPTIONS:".Bold().Info(), 1);
                foreach (var item in options.ToList())
                {
                    DisplayFormatted($"{" ".Generate(5)}{item.Text.Primary()}", $"{item.Description.Secondary().Italic()}", 1);
                    if (!string.IsNullOrEmpty(item.Usage))
                    {
                        // DisplayFormatted($"{"usage:".Primary()}", $"{item.Usage.Secondary()}", 1);
                    }
                }
            }
        }

        public void DisplayParameters(IReadOnlyCollection<HelpText> parameters)
        {
            if (parameters?.Any() == true)
            {
                Display($"PARAMETERS:".Bold().Info(), 1);
                foreach (var item in parameters.ToList())
                {
                    DisplayFormatted($"{" ".Generate(5)}{item.Text.Primary()}", $"{item.Description.Secondary().Italic()}", 1);
                }
            }
        }

        public void DisplayUsage(CommandHelpText commandHelpText)
        {
            commandHelpText.DisplayIf(x => x.CommandUsage,
                 $"{"Usage:\t".Info()}{commandHelpText.CommandUsage.Light()}",
                 (text) => Console.WriteLine(text));
        }

        private static void Display(string text, int tabs = 0)
        {
            Console.WriteLine($"{"\t".Generate(tabs)}{text}");
        }

        private static void Display(ANSIString text, int tabs = 0)
        {
            Console.WriteLine($"{"\t".Generate(tabs)}{text}");
        }

        private static void DisplayFormatted(string text, string description, int tabs = 0)
        {
            Console.WriteLine("\t".Generate(tabs) + "{0,-60}\t{1,-150}", text, description);
        }
    }
}