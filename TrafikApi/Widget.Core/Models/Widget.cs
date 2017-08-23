using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Widget.Core.Models
{
    public class Widget : WidgetBase
    {
        public ObjectId Id { get; set; }

        public WidgetView ToView()
        {
            return new WidgetView
            {
                Id = Id.ToString(),
                Type = Type,
                Title = Title,
                Position = Position
            };
        }
    }

    
}
