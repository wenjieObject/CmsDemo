using Microsoft.EntityFrameworkCore;
using StackExchange.Redis.Extensions.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;

namespace Czar.Cms.Models
{
    public static class EntityFrameworkCoreExtensions
    {
        public static DataTable GetDataTable(this IDbContextCore context, string sql, params DbParameter[] parameters)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var db = context.GetDatabase();
            db.EnsureCreated();
            var connection = db.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            var ds = new DataSet();
            var dt = new DataTable();
            DbCommand cmd;
            DataAdapter da;
            if (db.IsSqlServer())
            {
                cmd = new SqlCommand(sql, (SqlConnection)connection);
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                da = new SqlDataAdapter((SqlCommand)cmd);
            }
            //else if (db.IsMySql())
            //{
            //    cmd = new MySqlCommand(sql, (MySqlConnection)connection);
            //    if (parameters != null && parameters.Length > 0)
            //    {
            //        cmd.Parameters.AddRange(parameters);
            //    }

            //    da = new MySqlDataAdapter((MySqlCommand)cmd);
            //}
            //else if (db.IsNpgsql())
            //{
            //    cmd = new NpgsqlCommand(sql, (NpgsqlConnection)connection);
            //    if (parameters != null && parameters.Length > 0)
            //    {
            //        cmd.Parameters.AddRange(parameters);
            //    }

            //    da = new NpgsqlDataAdapter((NpgsqlCommand)cmd);
            //}
            //else if (db.IsSqlite())
            //{
            //    cmd = new SqliteCommand(sql, (SqliteConnection)connection);
            //    if (parameters != null && parameters.Length > 0)
            //    {
            //        cmd.Parameters.AddRange(parameters);
            //    }

            //    dt = cmd.ExecuteReader().GetSchemaTable();
            //    cmd.Dispose();
            //    connection.Close();
            //    return dt;
            //}
            //else if (db.IsOracle())
            //{
            //    cmd = new OracleCommand(sql, (OracleConnection)connection);
            //    if (parameters != null && parameters.Length > 0)
            //    {
            //        cmd.Parameters.AddRange(parameters);
            //    }

            //    da = new OracleDataAdapter((OracleCommand)cmd);
            //}
            else
            {
                throw new NotSupportedException("This method does not support current database yet.");
            }

            da.Fill(ds);
            dt = ds.Tables[0];
            da.Dispose();
            connection.Close();
            return dt;
        }

        public static DataTable GetCurrentDatabaseAllTables(this IDbContextCore context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var db = context.GetDatabase();
            var sql = string.Empty;
            if (db.IsSqlServer())
            {
                sql = "select * from (SELECT (case when a.colorder=1 then d.name else '' end) as TableName," +
                      "(case when a.colorder=1 then isnull(f.value,'') else '' end) as TableComment" +
                      " FROM syscolumns a" +
                      " inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'" +
                      " left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0) t" +
                      " where t.TableName!=''";
            }
            //else if (db.IsMySql())
            //{
            //    sql =
            //        "SELECT TABLE_NAME as TableName," +
            //        " Table_Comment as TableComment" +
            //        " FROM INFORMATION_SCHEMA.TABLES" +
            //        $" where TABLE_SCHEMA = '{db.GetDbConnection().Database}'";
            //}
            //else if (db.IsNpgsql())
            //{
            //    sql =
            //        "select relname as TableName," +
            //        " cast(obj_description(relfilenode,'pg_class') as varchar) as TableComment" +
            //        " from pg_class c" +
            //        " where relkind = 'r' and relname not like 'pg_%' and relname not like 'sql_%'" +
            //        " order by relname";
            //}
            //else if (db.IsOracle())
            //{
            //    sql =
            //        "select \"a\".TABLE_NAME as \"TableName\",\"b\".COMMENTS as \"TableComment\" from USER_TABLES \"a\" JOIN user_tab_comments \"b\" on \"b\".TABLE_NAME=\"a\".TABLE_NAME";
            //}
            else
            {
                throw new NotImplementedException("This method does not support current database yet.");
            }

            return context.GetDataTable(sql);
        }

        public static DataTable GetTableColumns(this IDbContextCore context, string tableName)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var db = context.GetDatabase();
            var sql = string.Empty;
            if (db.IsSqlServer())
            {
                sql = "SELECT a.name as ColName," +
                      "CONVERT(bit,(case when COLUMNPROPERTY(a.id,a.name,'IsIdentity')=1 then 1 else 0 end)) as IsIdentity, " +
                      "CONVERT(bit,(case when (SELECT count(*) FROM sysobjects  WHERE (name in (SELECT name FROM sysindexes  WHERE (id = a.id) AND (indid in  (SELECT indid FROM sysindexkeys  WHERE (id = a.id) AND (colid in  (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name)))))))  AND (xtype = 'PK'))>0 then 1 else 0 end)) as IsPrimaryKey," +
                      "b.name as ColumnType," +
                      "COLUMNPROPERTY(a.id,a.name,'PRECISION') as ColumnLength," +
                      "CONVERT(bit,(case when a.isnullable=1 then 1 else 0 end)) as IsNullable,  " +
                      "isnull(e.text,'') as DefaultValue," +
                      "isnull(g.[value], ' ') AS Comments " +
                      "FROM  syscolumns a left join systypes b on a.xtype=b.xusertype  inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id  left join sys.extended_properties g on a.id=g.major_id AND a.colid=g.minor_id left join sys.extended_properties f on d.id=f.class and f.minor_id=0 " +
                      $"where b.name is not null and d.name='{tableName}' order by a.id,a.colorder";
            }
            //else if (db.IsMySql())
            //{
            //    sql =
            //        "select column_name as ColName, " +
            //        " column_default as DefaultValue," +
            //        " IF(extra = 'auto_increment','TRUE','FALSE') as IsIdentity," +
            //        " IF(is_nullable = 'YES','TRUE','FALSE') as IsNullable," +
            //        " DATA_TYPE as ColumnType," +
            //        " CHARACTER_MAXIMUM_LENGTH as ColumnLength," +
            //        " IF(COLUMN_KEY = 'PRI','TRUE','FALSE') as IsPrimaryKey," +
            //        " COLUMN_COMMENT as Comments " +
            //        $" from information_schema.columns where table_schema = '{db.GetDbConnection().Database}' and table_name = '{tableName}'";
            //}
            //else if (db.IsNpgsql())
            //{
            //    sql =
            //        "select column_name as ColName," +
            //        "data_type as ColumnType," +
            //        "coalesce(character_maximum_length, numeric_precision, -1) as ColumnLength," +
            //        "CAST((case is_nullable when 'NO' then 0 else 1 end) as bool) as IsNullable," +
            //        "column_default as DefaultValue," +
            //        "CAST((case when position('nextval' in column_default)> 0 then 1 else 0 end) as bool) as IsIdentity, " +
            //        "CAST((case when b.pk_name is null then 0 else 1 end) as bool) as IsPrimaryKey," +
            //        "c.DeText as Comments" +
            //        " from information_schema.columns" +
            //        " left join " +
            //        " (select pg_attr.attname as colname,pg_constraint.conname as pk_name from pg_constraint " +
            //        " inner join pg_class on pg_constraint.conrelid = pg_class.oid" +
            //        " inner join pg_attribute pg_attr on pg_attr.attrelid = pg_class.oid and  pg_attr.attnum = pg_constraint.conkey[1]" +
            //        $" inner join pg_type on pg_type.oid = pg_attr.atttypid where pg_class.relname = '{tableName}' and pg_constraint.contype = 'p') b on b.colname = information_schema.columns.column_name " +
            //        " left join " +
            //        " (select attname, description as DeText from pg_class " +
            //        " left join pg_attribute pg_attr on pg_attr.attrelid = pg_class.oid" +
            //        " left join pg_description pg_desc on pg_desc.objoid = pg_attr.attrelid and pg_desc.objsubid = pg_attr.attnum " +
            //        $" where pg_attr.attnum > 0 and pg_attr.attrelid = pg_class.oid and pg_class.relname = '{tableName}') c on c.attname = information_schema.columns.column_name" +
            //        $" where table_schema = 'public' and table_name = '{tableName}' order by ordinal_position asc";
            //}
            //else if (db.IsOracle())
            //{
            //    sql = "select "
            //          + "a.DATA_LENGTH as ColumnLength,"
            //          + "a.COLUMN_NAME as ColName,"
            //          + "a.DATA_PRECISION as DataPrecision,"
            //          + "a.DATA_SCALE as DataScale,"
            //          + "a.DATA_TYPE as ColumnType,"
            //          + "decode(a.NULLABLE, 'Y', 'TRUE', 'N', 'FALSE') as IsNullable,"
            //          + "case when d.COLUMN_NAME is null then 'FALSE' else 'TRUE' end as IsPrimaryKey,"
            //          + "decode(a.IDENTITY_COLUMN, 'YES', 'TRUE', 'NO', 'FALSE') as IsIdentity,"
            //          + "b.COMMENTS as Comments "
            //          + "from user_tab_columns a "
            //          + "left join user_tab_comments b on b.TABLE_NAME = a.COLUMN_NAME "
            //          + "left join user_constraints c on c.TABLE_NAME = a.TABLE_NAME and c.CONSTRAINT_TYPE = 'P' "
            //          + "left join user_cons_columns d on d.CONSTRAINT_NAME = c.CONSTRAINT_NAME and d.COLUMN_NAME = a.COLUMN_NAME "
            //          + $"where a.Table_Name = '{tableName.ToUpper()}'";
            //}
            else
            {
                throw new NotImplementedException("This method does not support current database yet.");
            }

            return context.GetDataTable(sql);
        }

        public static IList<DbTable> GetCurrentDatabaseTableList(this IDbContextCore context)
        {
            var tables = context.GetCurrentDatabaseAllTables().ToList<DbTable>();
            var db = context.GetDatabase();
            DatabaseType dbType;
            if (db.IsSqlServer())
                dbType = DatabaseType.MSSQL;
            //else if (db.IsMySql())
            //    dbType = DatabaseType.MySQL;
            //else if (db.IsNpgsql())
            //{
            //    dbType = DatabaseType.PostgreSQL;
            //}
            //else if (db.IsOracle())
            //{
            //    dbType = DatabaseType.Oracle;
            //}
            else
            {
                throw new NotImplementedException("This method does not support current database yet.");
            }
            tables.ForEach(item =>
            {
                item.Columns = context.GetTableColumns(item.TableName).ToList<DbTableColumn>();
                item.Columns.ForEach(x =>
                {
                    var csharpType = DbColumnTypeCollection.DbColumnDataTypes.FirstOrDefault(t =>
                        t.DatabaseType == dbType && t.ColumnTypes.Split(',').Any(p =>
                            p.Trim().Equals(x.ColumnType, StringComparison.OrdinalIgnoreCase)))?.CSharpType;
                    if (string.IsNullOrEmpty(csharpType))
                    {
                        throw new SqlTypeException($"未从字典中找到\"{x.ColumnType}\"对应的C#数据类型，请更新DbColumnTypeCollection类型映射字典。");
                    }

                    x.CSharpType = csharpType;
                });
            });
            return tables;
        }


        /// <summary>
        /// 类型转换（包含Nullable<>和非Nullable<>转换）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        private static object ChangeType(object value, Type conversionType)
        {
            // Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
            // checking properties on conversionType below.
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            } // end if

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType

            if (conversionType.IsGenericType &&
                conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                } // end if

                // It's a nullable type, and not null, so that means it can be converted to its underlying type,
                // so overwrite the passed-in conversion type with this underlying type
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);

                conversionType = nullableConverter.UnderlyingType;
            } // end if

            // Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
            // nullable type), pass the call on to Convert.ChangeType
            return Convert.ChangeType(value, conversionType);
        }
    }

    public enum DatabaseType
    {
        MSSQL,
        MySQL,
        PostgreSQL,
        SQLite,
        InMemory,
        Oracle,
        MariaDB,
        MyCat,
        Firebird,
        DB2,
        Access
    }

    public class DbColumnTypeCollection
    {


        public static IList<DbColumnDataType> DbColumnDataTypes => new List<DbColumnDataType>()
        {
            #region MSSQL，https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings

            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "bigint", CSharpType = "Int64"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "binary,image,varbinary(max),rowversion,timestamp,varbinary", CSharpType = "Byte[]"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "bit", CSharpType = "Boolean"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "char,nchar,text,ntext,varchar,nvarchar,xml", CSharpType = "String"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "date,datetime,datetime2,smalldatetime", CSharpType ="DateTime"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "datetimeoffset", CSharpType ="DateTimeOffset"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "decimal,money,numeric,smallmoney", CSharpType ="Decimal"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "float", CSharpType ="Double"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "int", CSharpType ="Int32"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "real", CSharpType ="Single"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "smallint", CSharpType ="Int16"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "sql_variant", CSharpType ="Object"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "time", CSharpType ="TimeSpan"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "tinyint", CSharpType ="Byte"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MSSQL, ColumnTypes = "uniqueidentifier", CSharpType ="Guid"},

            #endregion

            #region PostgreSQL，http://www.npgsql.org/doc/types/basic.html

            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "boolean,bit(1)", CSharpType ="Boolean"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "smallint", CSharpType ="short"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "integer", CSharpType ="int"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "bigint", CSharpType ="long"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "real", CSharpType ="float"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "double precision", CSharpType ="Double"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "numeric,money", CSharpType ="decimal"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "text,character,character varying,citext,json,jsonb,xml,name", CSharpType ="String"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "point", CSharpType ="NpgsqlTypes.NpgsqlPoint"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "lseg", CSharpType ="NpgsqlTypes.NpgsqlLSeg"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "path", CSharpType ="NpgsqlTypes.NpgsqlPath"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "polygon", CSharpType ="NpgsqlTypes.NpgsqlPolygon"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "line", CSharpType ="NpgsqlTypes.NpgsqlLine"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "circle", CSharpType ="NpgsqlTypes.NpgsqlCircle"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "box", CSharpType ="NpgsqlTypes.NpgsqlBox"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "bit(n),bit varying", CSharpType ="BitArray"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "hstore", CSharpType ="IDictionary<string, string>"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "uuid", CSharpType ="Guid"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "cidr", CSharpType ="ValueTuple<IPAddress, int>"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "inet", CSharpType ="IPAddress"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "macaddr", CSharpType ="PhysicalAddress"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "tsquery", CSharpType ="NpgsqlTypes.NpgsqlTsQuery"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "tsvector", CSharpType ="NpgsqlTypes.NpgsqlTsVector"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "date,timestamp,timestamp with time zone,timestamp without time zone", CSharpType ="DateTime"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "interval,time", CSharpType ="TimeSpan"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "time with time zone", CSharpType ="DateTimeOffset"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "bytea", CSharpType ="Byte[]"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "oid,xid,cid", CSharpType ="uint"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "oidvector", CSharpType ="uint[]"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "char", CSharpType ="char"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "geometry", CSharpType ="NpgsqlTypes.PostgisGeometry"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.PostgreSQL, ColumnTypes = "record", CSharpType ="object[]"},

            #endregion

            #region MySQL，https://www.devart.com/dotconnect/mysql/docs/DataTypeMapping.html

            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "bool,boolean,bit(1),tinyint(1)", CSharpType ="Boolean"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "tinyint", CSharpType ="SByte"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "tinyint unsigned", CSharpType ="Byte"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "smallint, year", CSharpType ="Int16"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "int, integer, smallint unsigned, mediumint", CSharpType ="Int32"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "bigint, int unsigned, integer unsigned, bit", CSharpType ="Int64"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "float", CSharpType ="Single"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "double, real", CSharpType ="Double"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "decimal, numeric, dec, fixed, bigint unsigned, float unsigned, double unsigned, serial", CSharpType ="Decimal"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "date, timestamp, datetime", CSharpType ="DateTime"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "datetimeoffset", CSharpType ="DateTimeOffset"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "time", CSharpType ="TimeSpan"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "char, varchar, tinytext, text, mediumtext, longtext, set, enum, nchar, national char, nvarchar, national varchar, character varying", CSharpType ="String"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "binary, varbinary, tinyblob, blob, mediumblob, longblob, char byte", CSharpType ="Byte[]"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "geometry", CSharpType ="System.Data.Spatial.DbGeometry"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.MySQL, ColumnTypes = "geometry", CSharpType ="System.Data.Spatial.DbGeography"},

            #endregion

            #region Oracle, https://docs.oracle.com/cd/E14435_01/win.111/e10927/featUDTs.htm#CJABAEDD

            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "BFILE,BLOB,NCLOB,CLOB,REFCURSOR", CSharpType ="Object"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "CHAR,NCHAR,VARCHAR2,NVARCHAR2,XMLTYPE,ROWID,LONG", CSharpType ="String"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "Byte", CSharpType ="Byte"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "LongRaw,Raw", CSharpType ="Binary"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "Decimal", CSharpType ="Decimal"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "Single", CSharpType ="Single"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "Double,FLOAT", CSharpType ="Double"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "Int16", CSharpType ="Int16"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "Int32", CSharpType ="Int32"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "Int64,IntervalYM", CSharpType ="Int64"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "DATE, TIMESTAMP, TimeStampLTZ,TimeStampTZ,TIMESTAMP(0),TIMESTAMP(1),TIMESTAMP(2),TIMESTAMP(3),TIMESTAMP(4),TIMESTAMP(5),TIMESTAMP(6),TIMESTAMP(7),TIMESTAMP(8),TIMESTAMP(9)", CSharpType ="DateTime"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "IntervalDS,INTERVAL DAY TO SECOND", CSharpType ="TimeSpan"},
            new DbColumnDataType(){ DatabaseType = DatabaseType.Oracle, ColumnTypes = "NUMBER", CSharpType ="Decimal"},

            #endregion
        };
    }

    public class DbColumnDataType
    {
        public DatabaseType DatabaseType { get; set; }

        public string ColumnTypes { get; set; }

        public string CSharpType { get; set; }
    }


}
