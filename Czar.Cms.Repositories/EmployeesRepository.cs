using System;
using Czar.Cms.IRepositories;
using Czar.Cms.Models;

namespace Czar.Cms.Repositories
{
    public class EmployeesRepository : BaseRepository<Employees, Int32>, IEmployeesRepository
    {
        public EmployeesRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }
    }
}