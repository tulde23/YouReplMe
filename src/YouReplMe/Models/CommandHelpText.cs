using System.Linq.Expressions;
using System.Reflection;
using YouReplMe.Attributes;

namespace YouReplMe.Models
{
    public class CommandHelpTextBuilder<TVerb> where TVerb : RegexVerb

    {
        private CommandHelpText<TVerb> _helpText;

        public CommandHelpTextBuilder()
        {
            _helpText = new CommandHelpText<TVerb>();
        }

        public CommandHelpTextBuilder<TVerb> CommandName(string commandName)
        {
            _helpText.CommandName = commandName;
            return this;
        }

        public CommandHelpTextBuilder<TVerb> CommandDescription(string commandName = null)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                var description = Attribute.GetCustomAttribute(typeof(TVerb), typeof(DescriptionAttribute)) as DescriptionAttribute;
                _helpText.CommandDescription = description?.Description;
            }
            else
            {
                _helpText.CommandDescription = commandName;
            }
            return this;
        }

        public CommandHelpTextBuilder<TVerb> CommandUsage(Func<CommandHelpText<TVerb>, string> usageBuilder = null)
        {
            _helpText.CommandUsage = usageBuilder(_helpText);

            return this;
        }

        public CommandHelpTextBuilder<TVerb> CommandUsage<TAction>(TAction action) where TAction : AbstractAction<TVerb>
        {
            _helpText.CommandUsage = action.GetRegexBuilder().ToUsageString(_helpText.Options);

            return this;
        }

        public CommandHelpTextBuilder<TVerb> Parameters(Action<CommandHelpText<TVerb>, ParameterHelpTextBuilder<TVerb>> action)
        {
            action(_helpText, _helpText.ParametersBuilder);
            return this;
        }

        public CommandHelpTextBuilder<TVerb> Examples(Action<CommandHelpText<TVerb>, ExampleHelpTextBuilder<TVerb>> action)
        {
            action(_helpText, _helpText.ExamplesBuilder);
            return this;
        }

        public CommandHelpTextBuilder<TVerb> Options(Action<CommandHelpText<TVerb>, OptionsHelpTextBuilder<TVerb>> action)
        {
            action(_helpText, _helpText.OptionsBuilder);
            return this;
        }

        public CommandHelpText<TVerb> Build()
        {
            return _helpText;
        }
    }

    public class CommandHelpText
    {
        public string CommandName { get; set; }
        public string CommandDescription { get; set; }
        public string CommandUsage { get; set; }

        public virtual IReadOnlyCollection<HelpText> Parameters { get; protected set; }
        public virtual IReadOnlyCollection<HelpText> Options { get; protected set; }
        public virtual IReadOnlyCollection<HelpText> Examples { get; protected set; }
    }

    public class CommandHelpText<TVerb> : CommandHelpText where TVerb : RegexVerb
    {
        public ExampleHelpTextBuilder<TVerb> ExamplesBuilder { get; }
        public ParameterHelpTextBuilder<TVerb> ParametersBuilder { get; }
        public OptionsHelpTextBuilder<TVerb> OptionsBuilder { get; }

        public CommandHelpText()
        {
            ExamplesBuilder = new ExampleHelpTextBuilder<TVerb>();
            ParametersBuilder = new ParameterHelpTextBuilder<TVerb>();
            OptionsBuilder = new OptionsHelpTextBuilder<TVerb>();
        }

        public override IReadOnlyCollection<HelpText> Parameters => ParametersBuilder.Items();
        public override IReadOnlyCollection<HelpText> Options => OptionsBuilder.Items();
        public override IReadOnlyCollection<HelpText> Examples => ExamplesBuilder.Items();
    }

    public enum FormatType
    {
        Minimal,
        Default,
        Compact,
        Interesting,
    }

    public class HelpText
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public string Usage { get; set; }
    }

    public class HelpTextBuilder<TVerb> where TVerb : RegexVerb
    {
        protected List<HelpText> _helpText = new List<HelpText>();
        private static readonly Type _verbType = typeof(TVerb);
        private static readonly PropertyInfo[] _properties = typeof(TVerb).GetProperties();

        public virtual List<HelpText> Items()
        {
            return _helpText;
        }

        public HelpTextBuilder<TVerb> Add(string name, string description, string usage = null)
        {
            _helpText.Add(new HelpText { Text = name, Description = description, Usage = usage });
            return this;
        }

        public HelpTextBuilder<TVerb> Add(Expression<Func<TVerb, bool>> factory, string description = null, string usage = null)
        {
            var property = System.PropertyInfoExtensions.GetPropertyInfo(factory);
            var attribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;
            var descriptions = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute;
            _helpText.Add(new HelpText { Text = $"{attribute.PropertyName}", Description = descriptions?.Description ?? description, Usage = usage });
            return this;
        }

        public HelpTextBuilder<TVerb> Add(Expression<Func<TVerb, string>> factory, string description = null, string usage = null)
        {
            var property = System.PropertyInfoExtensions.GetPropertyInfo(factory);
            var attribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;
            var descriptions = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute;
            _helpText.Add(new HelpText { Text = $"{attribute.PropertyName}", Description = descriptions?.Description ?? description, Usage = usage });
            return this;
        }

        protected HelpTextBuilder<TVerb> FromVerb<TAttribute>() where TAttribute : HelpAttribute
        {
            var attributes = Attribute.GetCustomAttributes(typeof(TVerb), true);
            foreach (var attribute in attributes)
            {
                if (attribute is TAttribute)
                {
                    var exampleAttribute = (TAttribute)attribute;
                    _helpText.Add(new HelpText
                    {
                        Text = exampleAttribute.Name,
                        Description = exampleAttribute.Description,
                        Usage = exampleAttribute.Usages
                    });
                }
            }

            foreach (var property in _properties)
            {
                attributes = Attribute.GetCustomAttributes(property, true);
                foreach (var attribute in attributes)
                {
                    if (attribute is TAttribute)
                    {
                        var pa = attribute as ParameterAttribute;
                        var exampleAttribute = (TAttribute)attribute;
                        _helpText.Add(new HelpText
                        {
                            Text = pa?.AsGroup == true ?  $"{exampleAttribute.Name} {exampleAttribute.Name.AsRegexGroupName()}" :  exampleAttribute.Name,
                            Description = exampleAttribute.Description,
                            Usage = exampleAttribute.Usages
                        });
                    }
                }
            }
            return this;
        }
    }

    public class OptionsHelpTextBuilder<TVerb> : HelpTextBuilder<TVerb> where TVerb : RegexVerb
    {
        public override List<HelpText> Items()
        {
            var defaults = DefaultOptions.GetOptions();
            foreach (var d in defaults)
            {
                if (!_helpText.Any(x => x.Text == d.Text))
                {
                    _helpText.Add(d);
                }
            }

            return _helpText;
        }

        public OptionsHelpTextBuilder<TVerb> FromVerb()
        {
            base.FromVerb<OptionAttribute>();
            return this;
        }
    }

    public class ParameterHelpTextBuilder<TVerb> : HelpTextBuilder<TVerb> where TVerb : RegexVerb
    {
        /// <summary>
        /// Takes name and wraps as regex group
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="usage">The usage.</param>
        /// <returns></returns>
        public ParameterHelpTextBuilder<TVerb> AddAsParameter(string name, string description = null, string usage = null)
        {
            _helpText.Add(new HelpText { Text = $"{name} {name.AsRegexGroupName()}", Description = description, Usage = usage });
            return this;
        }

        public ParameterHelpTextBuilder<TVerb> AddAsParameter(Expression<Func<TVerb, object>> factory, string description = null, string usage = null)
        {
            var property = System.PropertyInfoExtensions.GetPropertyInfo(factory);
            var attribute = Attribute.GetCustomAttribute(property, typeof(JsonPropertyAttribute)) as JsonPropertyAttribute;
            var descriptions = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute;

            _helpText.Add(new HelpText { Text = $"{attribute.PropertyName} {attribute.PropertyName.AsRegexGroupName()}", Description = descriptions?.Description ?? description, Usage = usage });
            return this;
        }

        public ParameterHelpTextBuilder<TVerb> FromVerb()
        {
            base.FromVerb<ParameterAttribute>();
            return this;
        }
    }

    public class ExampleHelpTextBuilder<TVerb> : HelpTextBuilder<TVerb> where TVerb : RegexVerb
    {
        public ExampleHelpTextBuilder<TVerb> Add(string example, string description = null)
        {
            _helpText.Add(new HelpText { Text = example, Description = description });
            return this;
        }

        public ExampleHelpTextBuilder<TVerb> AddMany(params string[] examples)
        {
            examples.Tap(x => _helpText.Add(new HelpText { Text = x }));
            return this;
        }

        public ExampleHelpTextBuilder<TVerb> FromVerb()
        {
            base.FromVerb<ExampleAttribute>();
            return this;
        }
    }
}