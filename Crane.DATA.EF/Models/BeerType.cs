using System;
using System.Collections.Generic;

namespace Crane.DATA.EF.Models
{
    public partial class BeerType
    {
        public BeerType()
        {
            Beers = new HashSet<Beer>();
        }

        public int BeerTypeId { get; set; }
        public string BeerTypeName { get; set; } = null!;
        public string? BeerTypeDescription { get; set; }

        public virtual ICollection<Beer> Beers { get; set; }
    }
}
