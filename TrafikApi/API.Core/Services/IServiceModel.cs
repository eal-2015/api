using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services
{
    public interface IServiceModel
    {
        string Name { get; }
        string Url { get; }
        string Test { get; }
    }
}
