using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    class HelpCmd : BaseCommand
    {
        public HelpCmd()
        {
            Name = "Help";
            ParamList = null;
            Help = "<h3>Help</h3><p>Display the help page that list all comamnds.</p>";
            ParamDescriptionList = null;
            ParamRequired = null;
            Description = null;
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new HelpCmd());
        }

        override public bool Execute(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var handler in CommandManager.CommandHandlers.OrderBy( o => o.Name) )
            {
                sb.AppendLine(BuildCommandHelp(handler));
            }

            string fileName = Path.GetTempFileName();
            fileName = Path.ChangeExtension(fileName, ".html");

            string html = BuildHelpPage(sb.ToString());
            html.SaveAsTextFile(fileName);
            System.Diagnostics.Process.Start(fileName);

            return true; // hide command window
        }

        private string BuildCommandHelp(ICommand handler)
        {
            string retVal = handler.Help;
            if (retVal == null)
            {
                StringBuilder sb = new StringBuilder();
                int paramCnt = handler.ParamList == null ? 0 : handler.ParamList.Count();
                if (paramCnt > 0)
                {
                    sb.AppendLine("<dl>");
                    for (int i = 0; i < paramCnt; i++)
                    {
                        sb.AppendLine("<dt>");
                        sb.AppendLine(handler.ParamList[i]);
                        if (handler.ParamRequired.Count() > i)
                        {
                            if (handler.ParamRequired[i] == true)
                            {
                                sb.AppendLine(" <span class='label label-primary'>required</span> ");
                            }
                            else
                            {
                                sb.AppendLine(" <span class='label label-default'>optional</span> ");
                            }
                        }
                        sb.AppendLine("</dt>");
                        if (handler.ParamDescriptionList.Count() > i)
                        {
                            sb.AppendLine(string.Format("<dd>{0}</dd>", handler.ParamDescriptionList[i]));
                        }
                        else
                        {
                            sb.AppendLine("<dd></dd>");
                        }
                    }
                    sb.AppendLine("</dl>");
                }

                retVal = string.Format("<h3>{0}</h3><p>{1}</p>\r\n{2}", handler.Name, handler.Description, sb.ToString());
            }
            return retVal;
        }

        private string BuildHelpPage(string content)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset='utf-8' />");
            sb.AppendLine("    <meta name='viewport' content='width=device-width, initial-scale=1'>");
            sb.AppendLine("    <title>Sugar Help</title>");
            sb.AppendLine("    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css' integrity='sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7' crossorigin='anonymous'>");
            sb.AppendLine("    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap-theme.min.css' integrity='sha384-fLW2N01lMqjakBkx3l/M9EahuwpSfeNvV63J5ezn3uZzapT0u7EYsXMjQV+0En5r' crossorigin='anonymous'>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <div class='jumbotron text-center' style='background-color: #6f5499; color: white;' >");
            sb.AppendLine("        <h1>GET SH!T DONE with Sugar <span class='glyphicon glyphicon-asterisk' aria-hidden='true'></span></h1>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class='container'>");

            sb.AppendLine(content);

            sb.AppendLine("    </div>");
            sb.AppendLine("    <script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js' integrity='sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS' crossorigin='anonymous'></script>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
