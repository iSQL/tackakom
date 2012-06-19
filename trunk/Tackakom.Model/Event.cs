using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tackakom.Model
{
    public class Event
    {
        public int  Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TipUlaza { get; set; }
        public int HostId { get; set; }
        public int IconId { get; set; }
        public int EventCategoryId { get; set; }
    }
}
