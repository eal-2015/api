using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Widget.Core.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Widget.API.Controllers
{
    public class WidgetController : Facade
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> All()
        {
            var data = await GetModelsAsync<Core.Models.Widget>();
            var items = data.ToViewList();
            return Json(items);
        }

        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                string response = await client.GetStringAsync("http://localhost:10000/widget/all/");

                return Ok(response);
            }
            catch (Exception)
            {
                
            }            
            return Json(null);
        }
    }
}
