using System;
using System.Collections.Generic;

namespace Crane.DATA.EF.Models
{
    public partial class TeamMember
    {
        public int TeamMemberId { get; set; }
        public string TeamMemberName { get; set; } = null!;
        public string? TeamMemberTitle { get; set; }
        public string? TeamMemberDescription { get; set; }
        public string? TeamMemberImage { get; set; }
    }
}
