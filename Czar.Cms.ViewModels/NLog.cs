using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("NLog")]
	public class NLog:BaseModel<Int32>
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
		[Column("Application")]
		[MaxLength(50)]
		public String Application {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Logged")]
		[MaxLength(23)]
		public DateTime? Logged {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Level")]
		[MaxLength(50)]
		public String Level {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Message")]
		public String Message {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Logger")]
		[MaxLength(250)]
		public String Logger {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Callsite")]
		[MaxLength(512)]
		public String Callsite {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("Exception")]
		public String Exception {get;set;}



	}
}
