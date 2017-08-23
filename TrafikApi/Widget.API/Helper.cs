using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widget.API
{
    static class Helper
    {
        public static IActionResult JsonString(this Controller source, string data)
        {
            return source.Content(data, "application/json;charset=utf-8", Encoding.UTF8);
        }
    }
}
