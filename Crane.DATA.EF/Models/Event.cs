using System;
using System.Collections.Generic;

namespace Crane.DATA.EF.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string? EventDescripton { get; set; }
        public string? EventDate { get; set; }
        public string? EventTime { get; set; }
        public bool? IsReoccuring { get; set; }
        public string? EventImage { get; set; }
    }
}
