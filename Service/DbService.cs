using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Utility;
using Repositroy;
using Service.ViewModel;

namespace Service
{

    public class DbService
    {
        private IDbInfo _dbInfo;
        public DbService(IDbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }
        public List<TableInfoVm> GetTableName(string dbName)
        {
           return _dbInfo.GetDbTable(dbName).Select(d=>new TableInfoVm(){IsCheck =false,TableName =d.ToFirstUpper()}).ToList();
        }

        public List<ColumnInfo> GetColumnInfos(string dbName, string tableName)
        {
            return _dbInfo.GetColumnInfos(dbName, tableName);
        }
    }
}
