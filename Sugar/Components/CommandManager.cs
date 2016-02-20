using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSScriptLibrary;

namespace Sugar.Components.Commands
{
    public class CommandManager
    {
        static public List<ICommand> CommandHandlers = new List<ICommand>();

        static private void InitTasks()
        {
            foreach (Type taskHandler in Assembly.GetExecutingAssembly().GetTypes().Where(o => o.GetInterfaces().Contains(typeof(ITask))))
            {
                MethodInfo method = taskHandler.GetMethod("Init", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (method != null)
                {
                    method.Invoke(null, null);
                }
            }
        }

        static public void AddCommandHandler(ICommand handler)
        {
            CommandHandlers.Add(handler);
        }

        static public bool ExecuteCommand(string commandName)
        {
            bool retVal = false;
            foreach (var handler in CommandHandlers)
            {
                if (string.Compare(handler.Name, commandName, true) == 0)
                {
                    handler.Execute(args);
                    retVal = true;
                }
            }
            return retVal;
        }

        static public List<string> Search(string text)
        {
            List<string> retVal = new List<string>();

            if (string.IsNullOrWhiteSpace(text) == false)
            {
                foreach (var handler in CommandHandlers)
                {
                    if (handler.Name.StartsWith(text) == true)
                    {
                        retVal.Add(handler.Name);
                    }
                }
            }
            return retVal;
        }

        /*

                    if (handled == false)
                    {
                        switch (CommandText.ToUpper())
                        {
                            case "HIDE":
                                this.Hide();
                                handled = true;
                                break;
                            case "SHOW":
                                ShowClipboard();
                                CommandText = "";
                                handled = false;
                                break;
                            case "CLEAR":
                                ClipboardText = "";
                                handled = true;
                                break;
                            case "SHOW ON":
                                showCLipboard = true;
                                handled = true;
                                break;
                            case "SHOW OFF":
                                showCLipboard = false;
                                handled = true;
                                break;
                            default:
                                handled = CommandManager.ProcessCommand(CommandText);
                                break;
                        } 
 
                public string Execute(string text, string param)
                {
                    StringBuilder sb = new StringBuilder();
                    string[] lines = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                    List<string> list = new List<string>(lines);
                    foreach (var line in list.OrderBy(o => o).ToList())
                    {
                        sb.AppendLine(line.Trim());
                    }
                    return sb.ToString();
                }

                private string runScript(string text)
                {
                    string retVal = null;
                    try
                    {
                        dynamic script = CSScript.Evaluator
                                                 .CompileMethod("")
                                                 .CreateObject("*");
                        retVal = script.Execute(text, "");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //MessageBox.Show(ex.Message);
                    }
                    return retVal;
                }

                private void codeScrap()
                {
                    Console.WriteLine("Hot Key Pressed");


                    if (Clipboard.ContainsText())
                    {
                        string clipBoardText = Clipboard.GetText();
                        string newText = runScript(clipBoardText);
                        if (newText != null)
                        {
                            Clipboard.SetText(newText, TextDataFormat.Text);
                        }
                        Console.WriteLine(clipBoardText);
                        Console.WriteLine(newText);

                        // Use SendKeys to Paste
                        SendKeys.Send("^V");
                    }
                }


         */
    }
}
