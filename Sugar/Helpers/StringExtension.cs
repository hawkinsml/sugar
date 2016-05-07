using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public int HowMany(this string input, char letter )
        {
            int retVal = 0;
            foreach (char c in input.ToCharArray())
            {
                if (c == letter)
                {
                    retVal++;
                }
            }
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string SaveAsTextFile(this string input, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Path.GetTempFileName();
                fileName = Path.ChangeExtension(fileName, ".txt");
            }
            File.WriteAllText(fileName, input);
            return fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public int GetNumber(this string input)
        {
            int retVal = -1;
            if (!string.IsNullOrWhiteSpace(input))
            {
                Regex re = new Regex(@"\d+");
                Match m = re.Match(input);
                if (m.Success)
                {
                    try
                    {
                        retVal = int.Parse(m.Value);
                    }
                    catch (Exception) { }
                }
            }
            return retVal;
        }
    }
}
