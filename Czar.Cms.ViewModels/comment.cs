using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("comment")]
	public class Comment:BaseModel<Int32>
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
		[Column("content_id")]
		[Required]
		[MaxLength(10)]
		public Int32 ContentId {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("content")]
		[Required]
		[MaxLength(512)]
		public String Content {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("add_time")]
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}



	}
}
