using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCodeGenerator.Utility;

namespace Repository
{
    /// <summary>
    /// Class ColumnInfo.
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
         
        public string Type { get; set; }
         
        public int IsKey { get; set; }

        public bool AutoIncreased { get; set; }

        public bool IsNullable { get; set; }

        public string CSharpType { get {return MysqlDbTypeMap.MapCsharpType(Type); } }
    }

  
     
}
