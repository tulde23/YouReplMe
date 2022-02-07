using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouReplMe.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public  class ParameterAttribute : HelpAttribute
    {
        public bool AsGroup { get; set; }
        public ParameterAttribute(string text, string description, bool asGroup = false,  string usage = null) : base(text, description, usage)
        {
            AsGroup = asGroup;
        }
    }
}
