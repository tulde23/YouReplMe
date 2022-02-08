namespace YouReplMe.Contracts
{
    public interface IPrompt : ISingletonDependency
    {
        public bool Prompt(string message, params string[] options);
    }
}