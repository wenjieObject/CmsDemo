using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class TaskInfoRepository : BaseRepository<TaskInfo, Int32>, ITaskInfoRepository
    {
        public TaskInfoRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}