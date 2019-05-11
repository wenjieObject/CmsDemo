using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class MenuRepository : BaseRepository<Menu, Int32>, IMenuRepository
    {
        public MenuRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}