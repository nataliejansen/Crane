using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Crane.DATA.EF.Models //.Metadata
{
	#region BeerMetadata
	public class BeerMetadata
	{
        public int BeerId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        [Display(Name = "Beer Name")]
        public string BeerName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Must be 500 characters or less")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? BeerDescription { get; set; }

        [Range(0, short.MaxValue)]
        [Display(Name = "ABV %")]
        public decimal? BeerAbv { get; set; }

        [Display(Name = "On Tap?")]
        public bool IsOnTap { get; set; }

        [Display(Name = "Type of Beer")]
        public int BeerTypeId { get; set; }

        [StringLength(75)]
        [Display(Name = "Image")]
        public string? BeerImage { get; set; }

        [Display(Name = "Currently Active?")]
        public bool? IsCurrent { get; set; }
    }

    #endregion

    #region BeerTypeMetadata
    public class BeerTypeMetadata
    {
        public int BeerTypeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        [Display(Name = "Type of Beer")]
        public string BeerTypeName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Must be 500 characters or less")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? BeerTypeDescription { get; set; }
    }
    #endregion

    #region EventMetadata
    public class EventMetadata
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event Name is Required")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        [Display(Name = "Name of Event")]
        public string EventName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Must be 500 characters or less")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? EventDescripton { get; set; }

        [Display(Name = "Event Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Event Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 to 23:59")]
        public TimeSpan? EventTime { get; set; }

        [Display(Name = "Reoccuring Event?")]
        public bool? IsReoccuring { get; set; }

        [Display(Name = "Image")]
        public string? EventImage { get; set; }
    }

    #endregion

    #region MerchMetadata

    public class MerchMetadata
    {
        public int MerchId { get; set; }

        [Required(ErrorMessage = "Name of Merchandise is Required")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string MerchName { get; set; } = null!;

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Must be 500 characters or less")]
        [DataType(DataType.MultilineText)]
        public string? MerchDescription { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = false)]
        [Range(0, (double)decimal.MaxValue)]
        public decimal MerchPrice { get; set; }

        [Required(ErrorMessage = "Stock is Required")]
        [Range(0, short.MaxValue)]
        [Display(Name = "In Stock?")]
        public bool IsInStock { get; set; }

        [Required(ErrorMessage = "Merchandise Type is Required")]
        [Display(Name = "Type of Merchandise")]
        public int MerchTypeId { get; set; }

        [Display(Name = "Image")]
        public string? MerchImage { get; set; }
    }
    #endregion

    #region MerchTypeMetadata

    public class MerchTypeMetadata
    {
        public int MerchTypeId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string MerchName { get; set; } = null!;

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Must be 500 characters or less")]
        [DataType(DataType.MultilineText)]
        public string? MerchDescription { get; set; }
    }
    #endregion

    public class TeamMemberMetadata
    {
        public int TeamMemberId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string TeamMemberName { get; set; } = null!;

        [Display(Name = "Job Title")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string? TeamMemberTitle { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Must be 500 characters or less")]
        [DataType(DataType.MultilineText)]
        public string? TeamMemberDescription { get; set; }

        [Display(Name = "Image")]
        public string? TeamMemberImage { get; set; }
    }
}
