using Microsoft.Extensions.Hosting;

namespace YouReplMe.Contracts
{
    public interface IReplHost
    {
        public void DisplayPrompt(Func<string> logoText);

        public void DisplayLogo(Func<string> promptText);
        ValueTask StartAsync(IHost host, CancellationTokenSource token);
    }
}