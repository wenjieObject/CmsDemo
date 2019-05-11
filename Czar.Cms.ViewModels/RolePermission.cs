using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 角色权限表
	/// </summary>
	[Table("RolePermission")]
	public class RolePermission:BaseModel<Int32>
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		/// 角色主键
		/// </summary>
		[Column("RoleId")]
		[Required]
		[MaxLength(10)]
		public Int32 RoleId {get;set;}


		/// <summary>
		/// 菜单主键
		/// </summary>
		[Column("MenuId")]
		[Required]
		[MaxLength(10)]
		public Int32 MenuId {get;set;}


		/// <summary>
		/// 操作类型（功能权限）
		/// </summary>
		[Column("Permission")]
		[MaxLength(128)]
		public String Permission {get;set;}



	}
}
