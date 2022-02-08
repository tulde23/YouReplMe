namespace YouReplMe.Services
{
    internal class ConsolePrompt : IPrompt
    {
        public bool Prompt(string message, params string[] options)
        {
            if (options?.Any() != true)
            {
                options = new string[1] { "yes" };
            }
            Console.Write($"{message}  ({string.Join(",", options)})  ");
            var line = Console.ReadLine();
            return options.Contains(line, StringComparer.OrdinalIgnoreCase);
        }
    }
}