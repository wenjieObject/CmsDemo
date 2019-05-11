using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class EfMigrationsHistoryRepository : BaseRepository<EfMigrationsHistory, String>, IEfMigrationsHistoryRepository
    {
        public EfMigrationsHistoryRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}