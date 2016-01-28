using System.Text;

namespace Repository.Utility
{
    public static class DbHelper
    {
        public static string ToSplitFirstUpper(this string file)
        {
            var words = file.Split('_');
            var firstUpperWorld = new StringBuilder();
            foreach (var word in words)
            {
                var firstUpper = ToFirstUpper(word);
                firstUpperWorld.Append(firstUpper);
            }
            var firstUpperFile = firstUpperWorld.ToString().TrimEnd('_');
            return firstUpperFile;
        }

        // 将字符串设置成首字母大写  
        public static string ToFirstUpper(this string field)
        {
            var first = field.Substring(0, 1).ToUpperInvariant();
            var result = first;
            if (field.Length > 1)
            {
                var after = field.Substring(1);
                result = first + after;
            }
            return result;
        }
    }
}