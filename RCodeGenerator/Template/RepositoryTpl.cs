using System.Collections.Generic;
using System.Linq;
using Repository;

namespace RCodeGenerator.Template
{
    public partial class Repository
    { 
        public List<ColumnInfo> ColumnInfos { get; set; }

        public string NameSpace { get; set; }
        public string TableName { get; set; }

        public string UsingArea { get; set; }

        public bool IsKeyAutoIncreased
        {
            get
            { 
                var key = ColumnInfos.FirstOrDefault(d => d.IsKey==1);
                if (key==null)
                {
                    return false;
                }
                return key.AutoIncreased;
            }
        }

        public string KeyType
        {
            get
            {
                var key = ColumnInfos.FirstOrDefault(d => d.IsKey == 1);
                if (key == null)
                {
                    return typeof(int).Name;
                }
                return key.CSharpType;
            }
        }
    }
}