using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Czar.Cms.Models;

namespace Czar.Cms.Models
{
	/// <summary>
	/// 文章分类
	/// </summary>
	[Table("ArticleCategory")]
	public class ArticleCategory:BaseModel<Int32>
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column("Id")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Int32 Id {get;set;}


		/// <summary>
		/// 分类标题
		/// </summary>
		[Column("Title")]
		[Required]
		[MaxLength(128)]
		public String Title {get;set;}


		/// <summary>
		/// 父分类ID
		/// </summary>
		[Column("ParentId")]
		[Required]
		[MaxLength(10)]
		public Int32 ParentId {get;set;}


		/// <summary>
		/// 类别ID列表(逗号分隔开)
		/// </summary>
		[Column("ClassList")]
		[MaxLength(128)]
		public String ClassList {get;set;}


		/// <summary>
		/// 类别深度
		/// </summary>
		[Column("ClassLayer")]
		[MaxLength(10)]
		public Int32? ClassLayer {get;set;}


		/// <summary>
		/// 排序
		/// </summary>
		[Column("Sort")]
		[Required]
		[MaxLength(10)]
		public Int32 Sort {get;set;}


		/// <summary>
		/// 分类图标
		/// </summary>
		[Column("ImageUrl")]
		[MaxLength(128)]
		public String ImageUrl {get;set;}


		/// <summary>
		/// 分类SEO标题
		/// </summary>
		[Column("SeoTitle")]
		[MaxLength(128)]
		public String SeoTitle {get;set;}


		/// <summary>
		/// 分类SEO关键字
		/// </summary>
		[Column("SeoKeywords")]
		[MaxLength(256)]
		public String SeoKeywords {get;set;}


		/// <summary>
		/// 分类SEO描述
		/// </summary>
		[Column("SeoDescription")]
		[MaxLength(512)]
		public String SeoDescription {get;set;}


		/// <summary>
		/// 是否删除
		/// </summary>
		[Column("IsDeleted")]
		[Required]
		[MaxLength(1)]
		public Boolean IsDeleted {get;set;}



	}
}
