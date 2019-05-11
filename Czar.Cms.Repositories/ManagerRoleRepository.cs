using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class ManagerRoleRepository : BaseRepository<ManagerRole, Int32>, IManagerRoleRepository
    {
        public ManagerRoleRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}