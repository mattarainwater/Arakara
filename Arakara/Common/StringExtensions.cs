using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arakara.Common
{
    public static class StringExtensions
    {
        public static string WrapText(this string str, int charCount)
        {
            string[] originalLines = str.Split(new string[] { " " }, StringSplitOptions.None);

            List<string> wrappedLines = new List<string>();

            StringBuilder actualLine = new StringBuilder();
            double actualWidth = 0;

            foreach (var item in originalLines)
            {
                actualWidth += item.Count();

                if (actualWidth > charCount)
                {
                    wrappedLines.Add(actualLine.ToString());
                    actualLine.Clear();
                    actualWidth = item.Count();
                }
                actualLine.Append(item + " ");
            }

            if (actualLine.Length > 0)
            {
                wrappedLines.Add(actualLine.ToString());
            }

            return string.Join("\r\n", wrappedLines);
        }
    }
}
