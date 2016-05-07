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
    class HelpCmd : ICommand
    {
        static public void Init()
        {
            CommandManager.AddCommandHandler(new HelpCmd());
        }

        public string Name
        {
            get { return "Help"; }
        }

        public string[] ParamList
        {
            get { return null; }
        }

        public string Help
        {
            get { return "<h3>Help</h3><p>Display the help page that list all comamnds.</p>"; }
        }

        public bool Execute(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var handler in CommandManager.CommandHandlers.OrderBy( o => o.Name) )
            {
                sb.AppendLine(handler.Help);
            }

            string fileName = Path.GetTempFileName();
            fileName = Path.ChangeExtension(fileName, ".html");

            string html = BuildHelpPage(sb.ToString());
            html.SaveAsTextFile(fileName);
            System.Diagnostics.Process.Start(fileName);

            return true; // hide command window
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
