using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class ArticleCategoryRepository : BaseRepository<ArticleCategory, Int32>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}