namespace YouReplMe.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentifierAttribute : Attribute

    {
        public IdentifierAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}