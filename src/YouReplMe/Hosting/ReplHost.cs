using Microsoft.Extensions.Hosting;

namespace YouReplMe
{
    internal class ReplHost : IReplHost, ISingletonDependency
    {
        private readonly ICommandLineParser commandLineParser;

        public ReplHost(ICommandLineParser commandLineParser, IActionFactory actionFactory)
        {
            ActionFactory = actionFactory;

            this.commandLineParser = commandLineParser;
        }

        public IActionFactory ActionFactory { get; }

        public void DisplayLogo(Func<string> logoText)
        {
            var text = logoText?.Invoke();
            if (!string.IsNullOrEmpty(text))
            {
                Console.WriteLine(text.Primary());
            }
        }

        public void DisplayPrompt(Func<string> logoText)
        {
            var text = logoText?.Invoke();
            text = text ?? "do something... ";
            if (!string.IsNullOrEmpty(text))
            {
                Console.Write(text.Warning());
            }
        }

        public async ValueTask StartAsync(IHost host, CancellationTokenSource token)
        {
            try
            {
                var userInput = Console.ReadLine();
                var command = this.commandLineParser.Parse(userInput, token);

                if (command is ExitCommand)
                {
                    return;
                }
                if (command is ActionCommand)
                {
                    var action = ActionFactory.GetActionByKey(command.Action);
                    if (action == null)
                    {
                        Console.WriteLine("Invalid action " + command.Action.Danger());
                        return;
                    }
                    try
                    {
                        await action.RunAsync(userInput, token.Token);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.Danger());
                    }

                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.Danger());
            }
        }
    }
}