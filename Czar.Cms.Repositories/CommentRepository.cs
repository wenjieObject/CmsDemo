using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class CommentRepository : BaseRepository<Comment, Int32>, ICommentRepository
    {
        public CommentRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}