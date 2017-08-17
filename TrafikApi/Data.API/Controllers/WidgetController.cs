using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Widget.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Data.API.Controllers
{
    public class WidgetController : Facade
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get all widgets, with or without id(s)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> All(string[] id)
        {
            try
            {
                List<Widget.Core.Models.Widget> data = null;
                if (id != null && id.Length != 0)
                {
                    data = await GetModelsAsync<Widget.Core.Models.Widget>(a => id.Any(b => b.Equals(a.Id.ToString())));
                }
                else
                {
                    data = await GetModelsAsync<Widget.Core.Models.Widget>();
                }

                
                if (data != null && data.Count != 0)
                {
                    var items = data.ToViewList();
                    return Json(items);
                }
            }
            catch
            { }

            return Json(null);
        }

        public async Task<IActionResult> GetByTitle(string title)
        {
            try
            {
                if (!string.IsNullOrEmpty(title))
                {
                    var data = await GetModelAsync<Widget.Core.Models.Widget>(x => x.Title == title);
                    if (data != null)
                    {
                        return Json(data.ToView());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Json(null);
        }

        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var dataId = GetObjectId(id);
                    var data = await GetModelAsync<Widget.Core.Models.Widget>(x => x.Id.Equals(dataId));
                    if (data != null)
                    {
                        return Json(data.ToView());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Json(null);
        }

        public async Task<IActionResult> Create(WidgetBase model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AddModelAsync(model.ToModel());
                    return Json(1);
                }
                catch
                { }
            }

            return Json(null);
        }
    }
}
