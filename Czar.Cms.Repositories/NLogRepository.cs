using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class NLogRepository : BaseRepository<NLog, Int32>, INLogRepository
    {
        public NLogRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}