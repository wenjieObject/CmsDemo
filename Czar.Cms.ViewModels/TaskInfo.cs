using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 定时任务
	/// </summary>
	[Table("TaskInfo")]
	public class TaskInfo:BaseModel<Int32>
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Name")]
		[Required]
		[MaxLength(128)]
		public String Name {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Group")]
		[Required]
		[MaxLength(128)]
		public String Group {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Description")]
		[MaxLength(256)]
		public String Description {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Assembly")]
		[Required]
		[MaxLength(256)]
		public String Assembly {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("ClassName")]
		[Required]
		[MaxLength(256)]
		public String ClassName {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Status")]
		[Required]
		[MaxLength(10)]
		public Int32 Status {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Cron")]
		[Required]
		[MaxLength(128)]
		public String Cron {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("AddTime")]
		[MaxLength(23)]
		public DateTime? AddTime {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("AddManagerId")]
		[Required]
		[MaxLength(10)]
		public Int32 AddManagerId {get;set;}



	}
}
