using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouReplMe.Actions.Help
{
    internal static class DefaultOptions
    {
        public static IEnumerable<HelpText> GetOptions()
        {
            yield return new HelpText
            {
                Text = "verbose",
                Description = "enables verbose logging"
            };
            yield return new HelpText
            {
                Text = "whatif",
                Description = "allows you to simulate an action without making and permament changes."
            };
        }
    }
}
