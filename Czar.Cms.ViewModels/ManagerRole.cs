using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 后台管理员角色
	/// </summary>
	[Table("ManagerRole")]
	public class ManagerRole:BaseModel<Int32>
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		/// 角色名称
		/// </summary>
		[Column("RoleName")]
		[Required]
		[MaxLength(64)]
		public String RoleName {get;set;}


		/// <summary>
		/// 角色类型1超管2系管
		/// </summary>
		[Column("RoleType")]
		[Required]
		[MaxLength(10)]
		public Int32 RoleType {get;set;}


		/// <summary>
		/// 是否系统默认
		/// </summary>
		[Column("IsSystem")]
		[Required]
		[MaxLength(1)]
		public Boolean IsSystem {get;set;}


		/// <summary>
		/// 添加人
		/// </summary>
		[Column("AddManagerId")]
		[Required]
		[MaxLength(10)]
		public Int32 AddManagerId {get;set;}


		/// <summary>
		/// 添加时间
		/// </summary>
		[Column("AddTime")]
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}


		/// <summary>
		/// 修改人
		/// </summary>
		[Column("ModifyManagerId")]
		[MaxLength(10)]
		public Int32? ModifyManagerId {get;set;}


		/// <summary>
		/// 修改时间
		/// </summary>
		[Column("ModifyTime")]
		[MaxLength(23)]
		public DateTime? ModifyTime {get;set;}


		/// <summary>
		/// 是否删除
		/// </summary>
		[Column("IsDelete")]
		[Required]
		[MaxLength(1)]
		public Boolean IsDelete {get;set;}


		/// <summary>
		/// 备注
		/// </summary>
		[Column("Remark")]
		[MaxLength(128)]
		public String Remark {get;set;}



	}
}
