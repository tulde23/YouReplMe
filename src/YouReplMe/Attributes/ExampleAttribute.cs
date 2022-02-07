using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouReplMe.Attributes
{
    [AttributeUsage( AttributeTargets.Class, AllowMultiple =  true)]
    public  class ExampleAttribute : HelpAttribute
    {
        public ExampleAttribute( string text, string description) : base(text, description)
        {

        }
    }
}
