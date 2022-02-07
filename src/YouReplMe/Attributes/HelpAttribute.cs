namespace YouReplMe.Attributes
{
    public class HelpAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Usages { get; set; }

        public HelpAttribute(string name = null, string description = null, string usages = null)
        {
            Name = name;
            Description = description;
            Usages = usages;
        }
    }
}