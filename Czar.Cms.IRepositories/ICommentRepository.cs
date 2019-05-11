using System;
using Czar.Cms.Models;

namespace Czar.Cms.IRepositories
{
    public interface ICommentRepository:IRepository<Comment, Int32>
    {
    }
}