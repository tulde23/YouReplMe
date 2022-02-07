namespace YouReplMe.Contracts
{
    public interface IHelpAction
    {
        IEnumerable<CommandHelpText> HelpTextAsync(string commandLine);
    }
}