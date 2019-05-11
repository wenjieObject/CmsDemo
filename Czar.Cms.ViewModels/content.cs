using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("content")]
	public class Content:BaseModel<Int32>
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		[Column("id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("title")]
		[Required]
		[MaxLength(50)]
		public String Title {get;set;}


		///// <summary>
		/////  
		///// </summary>
		//[Column("content")]
		//[Required]
		//public String Content {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("status")]
		[Required]
		[MaxLength(10)]
		public Int32 Status {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("add_time")]
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("modify_time")]
		[MaxLength(23)]
		public DateTime? ModifyTime {get;set;}



	}
}
