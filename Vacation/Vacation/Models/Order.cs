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

	public partial class Order
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Order()
		{
			this.DetailOrders = new HashSet<DetailOrder>();
		}

		public int Id { get; set; }
		public Nullable<int> User_Id { get; set; }
		[DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:0.0}")]
		public decimal Total_Price { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public System.DateTime Created_at { get; set; }
		public string Status { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<DetailOrder> DetailOrders { get; set; }
		public virtual User User { get; set; }
	}
}
