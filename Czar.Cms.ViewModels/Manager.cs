using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 后台管理员
	/// </summary>
	[Table("Manager")]
	public class Manager:BaseModel<Int32>
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		/// 角色ID
		/// </summary>
		[Column("RoleId")]
		[Required]
		[MaxLength(10)]
		public Int32 RoleId {get;set;}


		/// <summary>
		/// 用户名
		/// </summary>
		[Column("UserName")]
		[Required]
		[MaxLength(32)]
		public String UserName {get;set;}


		/// <summary>
		/// 密码
		/// </summary>
		[Column("Password")]
		[Required]
		[MaxLength(128)]
		public String Password {get;set;}


		/// <summary>
		/// 头像
		/// </summary>
		[Column("Avatar")]
		[MaxLength(256)]
		public String Avatar {get;set;}


		/// <summary>
		/// 用户昵称
		/// </summary>
		[Column("NickName")]
		[MaxLength(32)]
		public String NickName {get;set;}


		/// <summary>
		/// 手机号码
		/// </summary>
		[Column("Mobile")]
		[MaxLength(16)]
		public String Mobile {get;set;}


		/// <summary>
		/// 邮箱地址
		/// </summary>
		[Column("Email")]
		[MaxLength(128)]
		public String Email {get;set;}


		/// <summary>
		/// 登录次数
		/// </summary>
		[Column("LoginCount")]
		[MaxLength(10)]
		public Int32? LoginCount {get;set;}


		/// <summary>
		/// 最后一次登录IP
		/// </summary>
		[Column("LoginLastIp")]
		[MaxLength(64)]
		public String LoginLastIp {get;set;}


		/// <summary>
		/// 最后一次登录时间
		/// </summary>
		[Column("LoginLastTime")]
		[MaxLength(23)]
		public DateTime? LoginLastTime {get;set;}


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
		/// 是否锁定
		/// </summary>
		[Column("IsLock")]
		[Required]
		[MaxLength(1)]
		public Boolean IsLock {get;set;}


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
