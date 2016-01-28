using System.Collections.Generic;

namespace Repository
{
    public interface IDbInfo
    {
        List<string> GetDbTable(string dbName);

        List<ColumnInfo> GetColumnInfos(string dbName,string tableName);
    }
}