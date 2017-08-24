using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services
{
    public interface IServiceAction
    {
        string Uri { get; set; }
        string Name { get; }
        IEnumerable<IServiceActionParamater> Parameters { get; }
    }
}
