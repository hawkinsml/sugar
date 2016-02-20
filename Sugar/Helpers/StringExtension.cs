using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Helpers
{
    public static class StringExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public List<string> SplitLines(this string input)
        {
            string[] lines = input.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            List<string> list = new List<string>(lines);

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public List<string> SplitParams(this string input)
        {
            string[] tokens = input.Split(new string[] { " " }, StringSplitOptions.None);
            List<string> list = new List<string>(tokens);

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public List<string> SplitTabs(this string input)
        {
            string[] tokens = input.Split(new string[] { "\t" }, StringSplitOptions.None);
            List<string> list = new List<string>(tokens);

            return list;
        }

    }
}
