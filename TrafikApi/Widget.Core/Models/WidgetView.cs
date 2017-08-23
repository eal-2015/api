using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Widget.Core.Models
{
    public class WidgetView : WidgetBase
    {
        public string Id { get; set; }

        public override Widget ToModel()
        {
            ObjectId id = ObjectId.Empty;
            ObjectId.TryParse(Id, out id);
            return new Widget
            {
                Id = id,
                Type = Type,
                Title = Title,
                Position = Position,
            };
        }
    }
}
