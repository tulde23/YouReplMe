using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouReplMe.Contracts
{
    public interface IChildActionActivator<TVerb> : ITransientDependency  where TVerb : RegexVerb 
    {

        IChildAction<TVerb> Activate(string key);
    }
}
