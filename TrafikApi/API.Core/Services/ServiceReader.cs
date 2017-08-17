using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace API.Core.Services
{
    class ServiceReader
    {
        public object ReadAll()
        {
            var controllers = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(x => x.Namespace.Contains("Controllers") && x.GetTypeInfo().IsClass && !x.Name.Equals("ServiceController"));

            foreach (var controller in controllers)
            {
                controller.GetTypeInfo().GetCustomAttributes<RouteAttribute>();
            }

            return null;
        }
    }
}
