using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    public class MakeUpper : BaseCommand
    {
        public MakeUpper()
        {
            Name = "Upper";
            ParamList = null;
            Help = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = "Converts the contents of the clipboard to all uppercase.";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new MakeUpper());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if ( !string.IsNullOrWhiteSpace( text ) )
            {
                Clipboard.SetText(text.ToUpper(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }

    public class MakeLower : BaseCommand
    {
        public MakeLower()
        {
            Name = "Lower";
            Help = null;
            ParamList = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = "Converts the contents of the clipboard to all lowercase.";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new MakeLower());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(text))
            {
                Clipboard.SetText(text.ToLower(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }

    public class Trim : BaseCommand
    {
        public Trim ()
        {
            Name = "Trim";
            ParamList = null;
            Help = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = "Trims spaces from the begining and ending of each line on the clipboard.";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Trim());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(text))
            {
                StringBuilder sb = new StringBuilder();

                List<string> lines = text.SplitLines();

                foreach (var line in lines)
                {
                    sb.AppendLine(line.Trim());
                }

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }


    public class Strip : BaseCommand
    {
        public Strip()
        {
            Name = "Strip";
            ParamList = null;
            Help = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = "Rempove carriage returns from text.";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Strip());
        }

         override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(text))
            {
                StringBuilder sb = new StringBuilder();

                List<string> lines = text.SplitLines();

                foreach (var line in lines)
                {
                    sb.Append(line.Trim());
                    sb.Append(" ");
                }

                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }

    public class Escape : BaseCommand
    {
        public Escape()
        {
            Name = "Escape";
            ParamList = null;
            Help = null;
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = "Escape a string to be put inside another string. Adds backslashing to double quotes.";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new Escape());
        }

        override public bool Execute(string[] args)
        {
            string text = Clipboard.GetText();
            if (!string.IsNullOrWhiteSpace(text))
            {
                bool decode = false;

                if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
                {
                    decode = args[1].ToLower().StartsWith("d");
                }

                StringBuilder sb = new StringBuilder();
                if (decode)
                {
                    for (int i = 0; i < text.Length; i++ )
                    {
                        switch( text[i] )
                        {
                            case '\\':
                                if ( i+1 < text.Length )
                                {
                                    switch(text[i+1])
                                    {
                                        case '"':
                                            sb.Append('"');
                                            i++;
                                            break;
                                        case 'n':
                                            sb.AppendLine();
                                            i++;
                                            break;
                                    }
                                }
                                else
                                {
                                    sb.Append(text[i]);
                                }
                                break;
                            default:
                                sb.Append(text[i]);
                                break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        switch (text[i])
                        {
                            case '"':
                                sb.Append('\\');
                                sb.Append('"');
                                break;
                            case '\n':
                                sb.Append('\\');
                                sb.Append('n');
                                break;
                            default:
                                sb.Append(text[i]);
                                break;
                        }
                    }
                }


                Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
            }
            return true; // hide command window
        }
    }

}
