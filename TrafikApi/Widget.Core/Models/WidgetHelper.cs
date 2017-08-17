using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Widget.Core.Models
{
    public static class GlobalWidgetHelper
    {
        public static List<WidgetView> ToViewList(this IEnumerable<Widget> source)
        {
            if (source != null)
            {
                var items = new List<WidgetView>();
                foreach (var item in source)
                {
                    items.Add(item.ToView());
                }
                return items;
            }
            return null;
        }

        
    }
}
