using System;
using System.Collections.Generic;

namespace Crane.DATA.EF.Models
{
    public partial class MerchType
    {
        public MerchType()
        {
            Merches = new HashSet<Merch>();
        }

        public int MerchTypeId { get; set; }
        public string MerchName { get; set; } = null!;
        public string? MerchDescription { get; set; }

        public virtual ICollection<Merch> Merches { get; set; }
    }
}
