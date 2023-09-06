using System;
using System.Collections.Generic;

namespace Crane.DATA.EF.Models
{
    public partial class Merch
    {
        public int MerchId { get; set; }
        public string MerchName { get; set; } = null!;
        public string? MerchDescription { get; set; }
        public decimal MerchPrice { get; set; }
        public bool IsInStock { get; set; }
        public int MerchTypeId { get; set; }
        public string? MerchImage { get; set; }

        public virtual MerchType MerchType { get; set; } = null!;
    }
}
