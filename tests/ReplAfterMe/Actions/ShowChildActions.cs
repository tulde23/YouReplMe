using Dependous;
using Newtonsoft.Json;
using System.ComponentModel;
using YouReplMe;
using YouReplMe.Actions;
using YouReplMe.Attributes;
using YouReplMe.Contracts;
using YouReplMe.Verbs;

namespace ReplAfterMe.Actions
{
    [NamedDependency(Key)]
    internal class ShowChildActions : AbstractAction<ChildActionVerb>
    {
        public ShowChildActions(IHelpTextBuilderFactory helpTextBuilderFactory, IChildActionActivator<ChildActionVerb> childActionActivator) : base(helpTextBuilderFactory)
        {
            this.childActionActivator = childActionActivator;
        }

        public const string Key = "child-action";
        private readonly IChildActionActivator<ChildActionVerb> childActionActivator;

        public override string ActionName => Key;

        protected override RegexBuilder<ChildActionVerb> BuildRegex(RegexBuilder<ChildActionVerb> builder)
        {
            return builder.GroupWithIdentifier(x => x.Action);
        }

        protected override async Task RunAsync(ChildActionVerb verb, CancellationToken cancellationToken)
        {
            var childAction = childActionActivator.Activate(verb.Action);
            await childAction?.RunAsync(verb, cancellationToken);
        }
    }

    [Description("an example of how to use child actions")]
    public class ChildActionVerb : RegexVerb
    {
        [JsonProperty("action"), Parameter("action", "the action.  can be list|print")]
        public string Action { get; set; }
    }

    [NamedDependency(Key)]
    public class ChildAction1 : IChildAction<ChildActionVerb>
    {
        public const string Key = "list";

        public Task RunAsync(ChildActionVerb verb, CancellationToken cancellationToken)
        {
            Console.WriteLine($"You chose {Key}");
            return Task.CompletedTask;
        }
    }

    [NamedDependency(Key)]
    public class ChildAction2 : IChildAction<ChildActionVerb>
    {
        public const string Key = "print";

        public Task RunAsync(ChildActionVerb verb, CancellationToken cancellationToken)
        {
            Console.WriteLine($"You chose {Key}");
            return Task.CompletedTask;
        }
    }
}