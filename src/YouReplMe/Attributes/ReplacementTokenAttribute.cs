using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouReplMe.Attributes
{
    public  class ReplacementTokenAttribute : Attribute
    {
        public ReplacementTokenAttribute(string name)
        {
            Name = name;
            if(!Name.StartsWith("{"))
            {
                Name = $"{{{Name}}}";
            }
        }

        public string Name { get; }
    }
}
