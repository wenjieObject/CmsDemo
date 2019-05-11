using System;
using Czar.Cms.Models;

namespace Czar.Cms.IRepositories
{
    public interface IArticleRepository:IRepository<Article, Int32>
    {
    }
}