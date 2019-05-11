using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 文章
	/// </summary>
	[Table("Article")]
	public class Article:BaseModel<Int32>
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		/// 分类ID
		/// </summary>
		[Column("CategoryId")]
		[Required]
		[MaxLength(10)]
		public Int32 CategoryId {get;set;}


		/// <summary>
		/// 文章标题
		/// </summary>
		[Column("Title")]
		[Required]
		[MaxLength(128)]
		public String Title {get;set;}


		/// <summary>
		/// 图片地址
		/// </summary>
		[Column("ImageUrl")]
		[MaxLength(128)]
		public String ImageUrl {get;set;}


		/// <summary>
		/// 文章内容
		/// </summary>
		[Column("Content")]
		[MaxLength(2147483647)]
		public String Content {get;set;}


		/// <summary>
		/// 浏览次数
		/// </summary>
		[Column("ViewCount")]
		[Required]
		[MaxLength(10)]
		public Int32 ViewCount {get;set;}


		/// <summary>
		/// 排序
		/// </summary>
		[Column("Sort")]
		[Required]
		[MaxLength(10)]
		public Int32 Sort {get;set;}


		/// <summary>
		/// 作者
		/// </summary>
		[Column("Author")]
		[MaxLength(64)]
		public String Author {get;set;}


		/// <summary>
		/// 来源
		/// </summary>
		[Column("Source")]
		[MaxLength(128)]
		public String Source {get;set;}


		/// <summary>
		/// SEO标题
		/// </summary>
		[Column("SeoTitle")]
		[MaxLength(128)]
		public String SeoTitle {get;set;}


		/// <summary>
		/// SEO关键字
		/// </summary>
		[Column("SeoKeyword")]
		[MaxLength(256)]
		public String SeoKeyword {get;set;}


		/// <summary>
		/// SEO描述
		/// </summary>
		[Column("SeoDescription")]
		[MaxLength(512)]
		public String SeoDescription {get;set;}


		/// <summary>
		/// 添加人ID
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
		/// 修改人ID
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
		/// 是否置顶
		/// </summary>
		[Column("IsTop")]
		[Required]
		[MaxLength(1)]
		public Boolean IsTop {get;set;}


		/// <summary>
		/// 是否轮播显示
		/// </summary>
		[Column("IsSlide")]
		[Required]
		[MaxLength(1)]
		public Boolean IsSlide {get;set;}


		/// <summary>
		/// 是否热门
		/// </summary>
		[Column("IsRed")]
		[Required]
		[MaxLength(1)]
		public Boolean IsRed {get;set;}


		/// <summary>
		/// 是否发布
		/// </summary>
		[Column("IsPublish")]
		[Required]
		[MaxLength(1)]
		public Boolean IsPublish {get;set;}


		/// <summary>
		/// 是否删除
		/// </summary>
		[Column("IsDeleted")]
		[Required]
		[MaxLength(1)]
		public Boolean IsDeleted {get;set;}



	}
}
