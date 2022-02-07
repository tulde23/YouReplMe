using System.Text.RegularExpressions;

namespace YouReplMe.Contracts
{
    public interface ICommandLineParser : ITransientDependency
    {
        Command Parse(string input, CancellationTokenSource token);
    }

    public class CommandLineParser : ICommandLineParser
    {
        private static readonly HashSet<string> terminators = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "exit",
        "cancel",
        "end",
        "logout"
    };

        private static readonly Regex _helpRegx = new RegexBuilder()
              .Group("action")
              .Group("help", true).ToRegex();

        private readonly IHelpFactory helpFactory;
        private readonly IActionFactory actionFactory;
        private readonly IHelpTextBuilderFactory helpTextBuilderFactory;

        public CommandLineParser(IHelpFactory helpFactory, IActionFactory actionFactory, IHelpTextBuilderFactory helpTextBuilderFactory)
        {
            this.helpFactory = helpFactory;
            this.actionFactory = actionFactory;
            this.helpTextBuilderFactory = helpTextBuilderFactory;
        }

        public Command Parse(string input, CancellationTokenSource token)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            CommandLineArguments cmdArgs = _helpRegx;

            var command = cmdArgs.ToVerb<ExitCommand>(input);
            if (command == null)
            {
                return new ContinueCommand();
            }
            if (terminators.Contains(command.Action))
            {
                token.Cancel();
                return command;
            }

            if (command.Action == HelpAction.Key || command.Action == ListAction.Key)
            {
                var helpAction = this.helpFactory.CreateHelpAction(HelpAction.Key);
                var txt = helpAction.HelpTextAsync(command.Help).OrderBy(x => x.CommandName);
                foreach (var item in txt)
                {
                    helpTextBuilderFactory.DisplayHelpText(item, command.Action == ListAction.Key ? FormatType.Minimal : FormatType.Default);
                }
                return new HelpCommand();
            }
            else if (command.Help == HelpAction.Key)
            {
                var helpAction = this.helpFactory.CreateHelpAction(HelpAction.Key);
                helpAction.HelpTextAsync(command.Action)?.Tap(item =>
                    helpTextBuilderFactory.DisplayHelpText(item, command.Help == ListAction.Key ? FormatType.Minimal : FormatType.Default));
                return new HelpCommand();
            }
            return new ActionCommand
            {
                Action = command.Action
            };
        }
    }

    public class Command : RegexVerb
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("help")]
        public string Help { get; set; }
    }

    public class ActionCommand : Command
    {
    }

    public class ExitCommand : Command
    {
    }

    public class HelpCommand : Command
    {
    }

    public class ListCommand : Command
    {
    }

    public class ContinueCommand : Command
    {
    }
}