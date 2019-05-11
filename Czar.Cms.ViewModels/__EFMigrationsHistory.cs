using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 
	/// </summary>
	[Table("__EFMigrationsHistory")]
	public class EfMigrationsHistory:BaseModel<String>
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		[Column("MigrationId")]
		public override String Id {get;set;}


		/// <summary>
		///  
		/// </summary>
		[Column("ProductVersion")]
		[Required]
		[MaxLength(32)]
		public String ProductVersion {get;set;}



	}
}
