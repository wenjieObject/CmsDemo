using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class RolePermissionRepository : BaseRepository<RolePermission, Int32>, IRolePermissionRepository
    {
        public RolePermissionRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}