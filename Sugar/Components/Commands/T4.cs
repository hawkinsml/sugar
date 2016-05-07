﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class T4 : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new T4());
        }

        public string Name
        {
            get { return "T4"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>T4</h3><p>Build json to define object for T4 templates that build code first.</p>"; }
        }

        public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();

            string tableName = "";
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                tableName = args[1];
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("    {");
                sb.AppendLine("      \"name\": \"" + tableName + "\",");
                sb.AppendLine("      \"table\": \"" + tableName + "\",");
                sb.AppendLine("      \"plural\": \"" + tableName + "\",");
                sb.AppendLine("      \"exposeAPI\": true,");
                sb.AppendLine("      \"readSecurity\" : \"\",");
                sb.AppendLine("      \"writeSecurity\" : \"\",");
                sb.AppendLine("      \"deleteAction\" : \"none\",");
                sb.AppendLine("      \"columns\": [");

                List<string> lines = text.SplitLines();
                foreach (var line in lines)
                {
                    string[] words = line.Trim().Split(' ');
                    string name = words[0].TrimStart('[').TrimEnd(']');
                    string type = words[1];
                    bool id = words[2].StartsWith("IDENTITY") ? true : false;
                    bool nullable = words[2].StartsWith("NOT") ? false : true;
                    int size = 0;

                    switch (type)
                    {
                        case "[int]":
                            type = "int";
                            break;
                        case "[bit]":
                            type = "bool";
                            break;
                        case "[date]":
                            type = "date";
                            break;
                        case "[datetime]":
                            type = "DateTime";
                            break;
                        default:
                            if ( type.StartsWith("[nvarchar]" ) )
                            {
                                size = type.GetNumber();
                                type = "string";
                            }
                            else if (type.StartsWith("[decimal]"))
                            {
                                type = "decimal";
                            }
                            
                            break;
                    }


                    sb.AppendLine("        {");
                    sb.AppendLine(string.Format("          \"name\": \"{0}\",", name));
                    sb.AppendLine(string.Format("          \"type\": \"{0}\",", type));
                    if (type == "string")
                    {
                        sb.AppendLine(string.Format("          \"max\": {0},", size));
                    }
                    if (id)
                    {
                        sb.AppendLine("          \"id\": true,");
                    }
                    sb.AppendLine(string.Format("          \"nullable\": {0}", nullable ? "true" : "false" ));
                    sb.AppendLine("        },");
                }

                sb.AppendLine("      ]");
                sb.AppendLine("    }");

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }

            return true; // hide command window
        }
    }
}