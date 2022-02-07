namespace YouReplMe.Models
{
    public static class CommandHelpTextExtensions
    {
        public static void DisplayIf(this CommandHelpText help, Func<CommandHelpText, string> func, string text, Action<string> action)
        {
            var result = func(help);
            if (!string.IsNullOrEmpty(result))
            {
                action(text ?? result);
            }
        }
    }
}