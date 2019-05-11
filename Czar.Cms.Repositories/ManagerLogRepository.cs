using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class ManagerLogRepository : BaseRepository<ManagerLog, Int32>, IManagerLogRepository
    {
        public ManagerLogRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}