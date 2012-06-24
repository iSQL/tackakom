using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tackakom.Model
{
    public class Host
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public Guid UserID { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
