using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class Prettify : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new Prettify());
        }

        public string Name
        {
            get { return "Prettify"; }
        }

        public string[] ParamList
        {
            get { return new string[] { "XML | JSON" }; }
        }

        public string Help
        {
            get
            {
                return "<h3>Prettify</h3>" +
                    "<p>Format XML or JSON string to multiply lines and nice indents.</p>";
            }

        
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();

            bool json = false;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                json = args[1].StartsWith("j", StringComparison.OrdinalIgnoreCase);
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                string formatedText = json ? FormatJson(text) : FormatXml(text);

                try
                {
                    Clipboard.SetText(formatedText, TextDataFormat.Text);
                }
                catch (Exception) { }
            }
            return true; // hide command window
        }

        string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        private const string INDENT_STRING = "  ";
        public static string FormatJson(string str)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }

    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }
}
