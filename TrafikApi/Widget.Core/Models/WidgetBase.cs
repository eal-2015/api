using System;
using System.Collections.Generic;
using System.Text;

namespace Widget.Core.Models
{
    public class WidgetBase
    {
        public WidgetType Type { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }

        public virtual Widget ToModel()
        {
            return new Widget
            {
                Type = Type,
                Title = Title,
                Position = Position
            };
        }
    }
}
