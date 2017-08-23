using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services
{
    public interface IServiceActionParamater
    {
        string Key { get; }
        ServiceDataType DataType { get; }
    }
}
