using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace Crane.DATA.EF.Models //.Metadata
{
    [ModelMetadataType(typeof(BeerMetadata))]
    public partial class Beer { }

    [ModelMetadataType(typeof(BeerTypeMetadata))]
    public partial class BeerType { }

    [ModelMetadataType(typeof(EventMetadata))]
    public partial class Event { }

    [ModelMetadataType(typeof(MerchMetadata))]
    public partial class Merch { }

    [ModelMetadataType(typeof(MerchTypeMetadata))]
    public partial class MerchType { }

    [ModelMetadataType(typeof(TeamMemberMetadata))]
    public partial class TeamMember { }

}
