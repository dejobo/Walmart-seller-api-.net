using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPIOOP
{
    public interface IWMOperation
    {
        Authentication authentication { get; }
        IWMOperation Request<T>() where T : IWMOperation;
        IWMOperation Response<T>() where T : IWMOperation;
    }
}
