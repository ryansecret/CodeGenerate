using System.Collections.Generic;
using System.Linq;
using Repository;

namespace RCodeGenerator.Template
{
    public partial class Model
    {

        
        public Model(string tableName,List<ColumnInfo> columnInfos)
        {
            TableName = tableName;
            ColumnInfos = columnInfos;
        }
        public List<ColumnInfo> ColumnInfos { get; set; }

        public string NameSpace { get; set; }
        public string TableName { get; set; }

        public string UsingArea { get; set; }

        public string KeyType
        {
            get
            {
                var key = ColumnInfos.FirstOrDefault(d => d.IsKey == 1);
                if (key==null)
                {
                    return typeof (int).Name;
                }
                return key.CSharpType;
            }
        }

        public string Parameters
        {
            get
            { 
                return string.Join(",",
                    ColumnInfos.Select(d => string.Format("{0} {1}",GetNullablType(d),d.Name.ToLower())));
            }
        }
         
        string GetNullablType(ColumnInfo column)
        { 
            return string.Format("{0}{1}",column.CSharpType, column.IsNullable && column.CSharpType != "string" ? "?" : "");
        }
    }
}