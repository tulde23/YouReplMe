using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace YouReplMe
{
    public class RegexBuilder
    {
        private readonly StringBuilder builder = new StringBuilder();
        private readonly StringBuilder usageBuilder = new StringBuilder();

        private readonly HashSet<string> _hashedBuilder = new HashSet<string>();

        private void _AddIdentifier(string identifier)
        {
            var id = $"({identifier})";
            if (!_hashedBuilder.Contains(id))
            {
                builder.Append(id);
                usageBuilder.Append(identifier);
                _hashedBuilder.Add(id);
            }
        }
        private void _Whitespace()
        {
            builder.Append(@"(\W+)?");
            usageBuilder.Append(" ");
        }

        private void _Group(string name, bool optional = false)
        {

            if (!_hashedBuilder.Contains(name))
            {

                builder.Append(@"(?<" + name + @">(\S+))" + (optional ? "?" : ""));
                var option = optional ? "*" : "";
                usageBuilder.Append($"<{name}>{option}");
                Whitespace();
                _hashedBuilder.Add(name);
            }
    
        }

        private void _GroupWithIdentifier(string identifier, string name, bool optional = false)
        {
            if (!_hashedBuilder.Contains(name))
            {
                builder.Append(@"((" + identifier + @")(\W+)?(?<" + name + @">(\S+)))" + (optional ? "?" : ""));
                var option = optional ? "*" : "";
                usageBuilder.Append($"{identifier} <{name}>{option}");
                Whitespace();
                _hashedBuilder.Add(identifier);
            }
        
        }

        private void _Options()
        {
            if (!_hashedBuilder.Contains("options"))
            {
                builder.Append(@"(options(?<options>(.*)))?");
                usageBuilder.Append("--options <options>");
                _hashedBuilder.Add("options");
            }
          
        }
        public RegexBuilder SetAction(string action)
        {
            _AddIdentifier(action);
            Whitespace();

            return this;
        }
        public RegexBuilder AddIdentifier(string identifier)
        {
            _AddIdentifier(identifier);
            Whitespace();

            return this;
        }

        public RegexBuilder Whitespace()
        {
            _Whitespace();
            return this;
        }

        public RegexBuilder Group(string name, bool optional = false)
        {
            builder.Append(@"(?<" + name + @">(\S+))" + (optional ? "?" : ""));
            var option = optional ? "*" : "";
            usageBuilder.Append($"<{name}>{option}");
            Whitespace();
            return this;
        }

        public RegexBuilder GroupWithIdentifier(string identifier, string name, bool optional = false)
        {
            builder.Append(@"((" + identifier + @")(\W+)?(?<" + name + @">(\S+)))" + (optional ? "?" : ""));
            var option = optional ? "*" : "";
            usageBuilder.Append($"{identifier} <{name}>{option}");
            Whitespace();
            return this;
        }

        public RegexBuilder Options()
        {
            builder.Append(@"(options(?<options>(.*)))?");
            usageBuilder.Append("--options <options>");
            return this;
        }

        public Regex ToRegex()
        {
            return new Regex(builder.ToString());
        }

        public string ToUsageString(IEnumerable<HelpText> helpText)
        {
            if (helpText?.Any() == true)
            {
                usageBuilder.Replace("<options>", string.Join(",", helpText.Select(x => x.Text)));
            }
            return usageBuilder.ToString();
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }

    public class RegexBuilder<TVerb> : RegexBuilder where TVerb : RegexVerb
    {
        public RegexBuilder<TVerb> Group(Expression<Func<TVerb, string>> factory, bool optional)
        {
            var property = PropertyInfoExtensions.GetPropertyInfo(factory);
            var attribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;
            base.Group(attribute?.PropertyName ?? property.Name.ToCamelCase(), optional);
            return this;
        }

        public RegexBuilder<TVerb> AddIdentifier(Expression<Func<TVerb, string>> factory)
        {
            var property = PropertyInfoExtensions.GetPropertyInfo(factory);
            var attribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;
            base.AddIdentifier(attribute?.PropertyName ?? property.Name.ToCamelCase());

            return this;
        }

        public new RegexBuilder<TVerb> Options()
        {
            base.Options();
            return this;
        }

        public RegexBuilder<TVerb> GroupWithIdentifier(Expression<Func<TVerb, string>> factory, bool optional = false)
        {
            var property = PropertyInfoExtensions.GetPropertyInfo(factory);
            var attribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;

            var name = attribute?.PropertyName ?? property.Name.ToCamelCase();
            GroupWithIdentifier(name, name, optional);
            return this;
        }

        public new RegexBuilder<TVerb> SetAction(string action)
        {
            base.SetAction(action);

            return this;
        }

        public new RegexBuilder<TVerb> AddIdentifier(string identifier)
        {
            base.AddIdentifier(identifier);

            return this;
        }

        public new RegexBuilder<TVerb> Whitespace()
        {
            base.Whitespace();
            return this;
        }

        public new RegexBuilder<TVerb> Group(string name, bool optional = false)
        {
            base.Group(name, optional);
            return this;
        }

        public new RegexBuilder<TVerb> GroupWithIdentifier(string identifier, string name, bool optional = false)
        {
            base.GroupWithIdentifier(identifier, name, optional);
            return this;
        }
    }
}