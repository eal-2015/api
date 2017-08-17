using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services
{
    public struct ServiceData
    {
        public bool IsArray { get; }
        public ServiceDataType DataType { get; }

        public ServiceData(ServiceDataType type, bool isArray = false)
        {
            DataType = type;
            IsArray = isArray;
        }
    }
}
