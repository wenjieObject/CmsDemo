using Czar.Cms.DataBase;
using Czar.Cms.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Program();
            s.TestGenerateEntitiesForSqlServer();
            Console.WriteLine("Hello World!");
        }

        #region Test methods for SQL Server

        public  void TestGetDataTableForSqlServer()
        {
            BuildServiceForSqlServer();
            var dbContext = AspectCoreContainer.Resolve<IDbContextCore>();
            var dt1 = dbContext.GetCurrentDatabaseAllTables();
            foreach (DataRow row in dt1.Rows)
            {
                var dt2 = dbContext.GetTableColumns(row["TableName"].ToString());
            }
        }

        public void TestGetDataTableListForSqlServer()
        {
            BuildServiceForSqlServer();
            var dbContext = AspectCoreContainer.Resolve<IDbContextCore>();
            var tables = dbContext.GetCurrentDatabaseTableList();
        }

        public void TestGenerateEntitiesForSqlServer()
        {
            BuildServiceForSqlServer();
            CodeGenerator.GenerateAllCodesFromDatabase(true);
        }

        #endregion


        public  IServiceProvider BuildServiceForSqlServer()
        {
            IServiceCollection services = new ServiceCollection();

            //在这里注册EF上下文
            services = RegisterSqlServerContext(services);
            services.Configure<CodeGenerateOption>(options =>
            {
                options.ModelsNamespace = "Czar.Cms.Models";
                options.IRepositoriesNamespace = "Czar.Cms.IRepositories";
                options.RepositoriesNamespace = "Czar.Cms.Repositories";
                options.ControllersNamespace = "Czar.Cms.Controllers";
                options.OutputPath = "D:\\VSCode\\Czar.Cms";
            });
            //暂不使用redis
            //services.UseCsRedisClient(
            //    "127.0.0.1:6379,abortConnect=false,connectRetry=3,connectTimeout=3000,defaultDatabase=1,syncTimeout=3000,version=3.2.100,responseTimeout=3000");
            services.AddOptions();
            return AspectCoreContainer.BuildServiceProvider(services); //接入AspectCore.Injector
        }


        /// <summary>
        /// 注册SQLServer上下文
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection RegisterSqlServerContext(IServiceCollection services)
        {
            services.Configure<DbContextOption>(options =>
            {
                options.ConnectionString =
                    "Data Source=(local);Initial Catalog=DBTest;User ID=sa;pwd=123123123";
            });
            services.AddScoped<IDbContextCore, SqlServerDbContext>(); //注入EF上下文
            return services;
        }
    }
}
