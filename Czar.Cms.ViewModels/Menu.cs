using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 后台管理菜单
	/// </summary>
	[Table("Menu")]
	public class Menu:BaseModel<Int32>
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		/// 父菜单ID
		/// </summary>
		[Column("ParentId")]
		[Required]
		[MaxLength(10)]
		public Int32 ParentId {get;set;}


		/// <summary>
		/// 名称
		/// </summary>
		[Column("Name")]
		[Required]
		[MaxLength(32)]
		public String Name {get;set;}


		/// <summary>
		/// 显示名称
		/// </summary>
		[Column("DisplayName")]
		[MaxLength(128)]
		public String DisplayName {get;set;}


		/// <summary>
		/// 图标地址
		/// </summary>
		[Column("IconUrl")]
		[MaxLength(128)]
		public String IconUrl {get;set;}


		/// <summary>
		/// 链接地址
		/// </summary>
		[Column("LinkUrl")]
		[MaxLength(128)]
		public String LinkUrl {get;set;}


		/// <summary>
		/// 排序数字
		/// </summary>
		[Column("Sort")]
		[MaxLength(10)]
		public Int32? Sort {get;set;}


		/// <summary>
		/// 操作权限（按钮权限时使用）
		/// </summary>
		[Column("Permission")]
		[MaxLength(256)]
		public String Permission {get;set;}


		/// <summary>
		/// 是否显示
		/// </summary>
		[Column("IsDisplay")]
		[Required]
		[MaxLength(1)]
		public Boolean IsDisplay {get;set;}


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



	}
}
