using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SubEdit
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(args[0]);
            bool sub = args[1] == "sub";

            var r = new Regex(@"(\d\d):(\d\d):(\d\d),(\d{3})");
            var l = ParseTime(r.Match(args[2]));
            if (sub)
                l = -l;

            var sb = new StringBuilder();
            int i = 0;

            foreach(Match m in r.Matches(input))
            {
                for(; i < m.Index; i++)
                    sb.Append(input[i]);

                var t = ParseTime(m);
                var s = t + l;

                sb.AppendFormat("{0:00}:{1:00}:{2:00},{3:000}", s.Hours, s.Minutes, s.Seconds, s.Milliseconds);

                i += m.Length;
            }

            for (; i < input.Length; i++)
                sb.Append(input[i]);

                File.WriteAllText(args[0], sb.ToString());
        }

        static TimeSpan ParseTime(Match m)
        {
            return new TimeSpan(0, int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value));
        }
    }
}
