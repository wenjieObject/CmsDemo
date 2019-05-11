using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class ManagerRepository : BaseRepository<Manager, Int32>, IManagerRepository
    {
        public ManagerRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}