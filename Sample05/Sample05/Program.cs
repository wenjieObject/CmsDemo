using Dapper;
using Sample05.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sample05
{
    class Program
    {
        static void Main(string[] args)
        {
            //test_insert();
            //test_mult_insert();
            //test_update();
            //test_mult_update();
            test_mult_insert();
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 测试插入单条数据
        /// </summary>
        static void test_insert()
        {
            var content = new Content
            {
                title = "标题1",
                content = "内容1",

            };
            using (var conn = new SqlConnection("Data Source=(local);Initial Catalog=DBTest;User ID=sa;pwd=123123123;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [Content] (title, [content], status, add_time, modify_time)
                                     VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_insert：插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量插入两条数据
        /// </summary>
        static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id = 5,
                content = "批量插入内容1",

            },
               new Content
            {
                id = 5,
                content = "批量插入内容2",

            },
        };

            using (var conn = new SqlConnection("Data Source=(local);Initial Catalog=DBTest;User ID=sa;pwd=123123123;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [comment]
                (content_id, content,  add_time)
VALUES   (@id,@content,GETDATE())";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
            }
        }


        /// <summary>
        /// 测试修改单条数据
        /// </summary>
        static void test_update()
        {
            var content = new Content
            {
                id = 5,
                title = "标题5",
                content = "内容5",

            };
            using (var conn = new SqlConnection("Data Source=(local);Initial Catalog=DBTest;User ID=sa;pwd=123123123;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"UPDATE  [Content]
SET         title = @title, [content] = @content, modify_time = GETDATE()
WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_update：修改了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量修改多条数据
        /// </summary>
        static void test_mult_update()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=6,
                title = "批量修改标题6",
                content = "批量修改内容6",

            },
               new Content
            {
                id =7,
                title = "批量修改标题7",
                content = "批量修改内容7",

            },
        };

            using (var conn = new SqlConnection("Data Source=(local);Initial Catalog=DBTest;User ID=sa;pwd=123123123;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"UPDATE  [Content]
SET         title = @title, [content] = @content, modify_time = GETDATE()
WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_update：修改了{result}条数据！");
            }
        }
    }
}
