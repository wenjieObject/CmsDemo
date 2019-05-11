using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("Employees")]
	public class Employees:BaseModel<Int32>
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		[Column("ID")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Name")]
		public String Name {get;set;}



	}
}
