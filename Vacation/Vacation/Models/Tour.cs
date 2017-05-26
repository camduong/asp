//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vacation.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public partial class Tour
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Tour()
		{
			this.Images = new HashSet<Image>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int Location_ID { get; set; }
		public int NumberTicket { get; set; }
		[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public System.DateTime Depart_Date { get; set; }
		[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public System.DateTime Return_Date { get; set; }
		public int Day { get; set; }
		[DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:0.0}")]
		public decimal Price { get; set; }
		public string Schedule { get; set; }

		public virtual Location Location { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Image> Images { get; set; }
	}
}