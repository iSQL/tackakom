using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tackakom.Model
{
    public class EventCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { set; get; }
        public virtual ICollection<Event> Events { get; set; }

    }
}
