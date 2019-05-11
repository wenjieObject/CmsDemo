using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class ArticleRepository : BaseRepository<Article, Int32>, IArticleRepository
    {
        public ArticleRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}