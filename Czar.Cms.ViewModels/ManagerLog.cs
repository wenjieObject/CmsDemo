using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 操作日志
	/// </summary>
	[Table("ManagerLog")]
	public class ManagerLog:BaseModel<Int32>
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		/// 操作类型
		/// </summary>
		[Column("ActionType")]
		[MaxLength(32)]
		public String ActionType {get;set;}


		/// <summary>
		/// 主键
		/// </summary>
		[Column("AddManageId")]
		[Required]
		[MaxLength(10)]
		public Int32 AddManageId {get;set;}


		/// <summary>
		/// 操作人名称
		/// </summary>
		[Column("AddManagerNickName")]
		[MaxLength(64)]
		public String AddManagerNickName {get;set;}


		/// <summary>
		/// 操作时间
		/// </summary>
		[Column("AddTime")]
		[Required]
		[MaxLength(23)]
		public DateTime AddTime {get;set;}


		/// <summary>
		/// 操作IP
		/// </summary>
		[Column("AddIp")]
		[MaxLength(64)]
		public String AddIp {get;set;}


		/// <summary>
		/// 备注
		/// </summary>
		[Column("Remark")]
		[MaxLength(256)]
		public String Remark {get;set;}



	}
}
