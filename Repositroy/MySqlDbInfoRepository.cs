using System.Collections.Generic;
using System.Linq;
using Dapper;
using Repository;

namespace Repositroy
{
    public class MySqlDbInfoRepository:IDbInfo
    { 
        public List<string> GetDbTable(string dbName)
        {
            using (var con=Database.Open())
            {
                string sql =

                    "select table_name from information_schema.tables where table_schema=@dbName and table_type='base table'";
                        
                return con.Query<string>(sql,new{dbName}).ToList();
            }
        }
         
        public List<ColumnInfo> GetColumnInfos(string dbName,string tableName)
        {
            using (var con=Database.Open())
            {
                string sql = @"select t.data_type Type,case when t.column_key='PRI' then 1 else 0 end IsKey,t.column_name Name,t.column_name,case when t.is_nullable='YES' then 1 else 0 end   IsNullable,case when t.extra='auto_increment' then 1 else 0 end AutoIncreased  from information_schema.columns t where table_schema=@dbName and table_name=@tableName";
                return con.Query<ColumnInfo>(sql,new{dbName,tableName}).ToList();
            }
        }
    }
}