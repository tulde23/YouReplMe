using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouReplMe.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OptionAttribute : HelpAttribute
    {
        public OptionAttribute(string text, string description, string usage = null) : base(text, description, usage)
        {

        }
    }
}
