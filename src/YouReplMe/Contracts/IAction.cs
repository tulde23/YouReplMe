namespace YouReplMe.Contracts
{
    public interface IAction : IHelpProvider, ITransientDependency
    {

        Task RunAsync(string commandLine, CancellationToken cancellationToken);
    }

    public interface IActionFactory
    {
        IAction GetActionByKey(string key);

        /// <summary>
        /// Gets the help by key.
        ///
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        CommandHelpText GetHelpByKey(string key);

        IEnumerable<CommandHelpText> GetAllHelp();
    }
}