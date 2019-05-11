using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class ContentRepository : BaseRepository<Content, Int32>, IContentRepository
    {
        public ContentRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}