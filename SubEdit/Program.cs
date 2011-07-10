using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SubEdit
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] { "Red Cliff (2008).srt", "10:00:00,000" };

            string input = File.ReadAllText(args[0]);

            var r = new Regex(@"(\d\d):(\d\d):(\d\d),(\d{3})");
            var l = ParseTime(r.Match(args[1]));
            var sb = new StringBuilder();

            int i = 0;

            foreach(Match m in r.Matches(input))
            {
                for(; i < m.Index; i++)
                    sb.Append(input[i]);

                var t = ParseTime(m);

                sb.AppendFormat("{0:00}:{1:00}:{2:00}:{3:000}", t[0] + l[0], t[1] + l[1], t[2] + l[2], t[3] + l[3]);

                i += m.Length;
            }

            File.WriteAllText(args[0],sb.ToString());
        }

        static int[] ParseTime(Match m)
        {
            var ret = new int[4];
            for (int i = 0; i < 4; i++)
                ret[i] = int.Parse(m.Groups[i+1].Value);
            return ret;
        }
    }
}
