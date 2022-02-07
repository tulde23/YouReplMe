using System.Text.RegularExpressions;

namespace YouReplMe.Parsing
{
    public class CommandLineArguments
    {
        private readonly Regex args;

        public CommandLineArguments(Regex args)
        {
            this.args = args;
        }

        public T ToVerb<T>(string args) where T : RegexVerb
        {
            var results = new List<Dictionary<string, string>>();
            var matches = this.args.Matches(args);
            if (matches.Any())
            {
                var names = this.args.GetGroupNames();
                foreach (Match match in matches)
                {
                    var items = new Dictionary<string, string>(); ;
                    foreach (var name in names)
                    {
                        var group = match.Groups[name];
                        items.Add(name, group.Value);
                    }
                    results.Add(items);
                }
            }
            var verb = RegexVerb.Create<T>(results);
            return verb;
        }

        public static implicit operator CommandLineArguments(Regex args)
        {
            return new CommandLineArguments(args);
        }
    }


}