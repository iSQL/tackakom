using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Tackakom.Model
{
    public class Event
    {
        public int  Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Display(Name = "Naslov")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Entry { get; set; }
        public virtual Host Host { get; set; }
        public virtual EventCategory EventCategory { get; set; }
    }
}
