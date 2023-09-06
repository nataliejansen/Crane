using System;
using System.Collections.Generic;

namespace Crane.DATA.EF.Models
{
    public partial class Beer
    {
        public int BeerId { get; set; }
        public string BeerName { get; set; } = null!;
        public string? BeerDescription { get; set; }
        public decimal? BeerAbv { get; set; }
        public bool IsOnTap { get; set; }
        public int BeerTypeId { get; set; }
        public string? BeerImage { get; set; }
        public bool? IsCurrent { get; set; }

        public virtual BeerType BeerType { get; set; } = null!;
    }
}
