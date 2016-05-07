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

        static public void InitCommands()
        {
            foreach (Type taskHandler in Assembly.GetExecutingAssembly().GetTypes().Where(o => o.GetInterfaces().Contains(typeof(ICommand))))
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

        static public bool ExecuteCommand(string commandName, string param)
        {
            List<string> args = new List<string>();
            args.Add(commandName);
            args.Add(param);
            return ExecuteCommand(commandName, args.ToArray());
        }

        static public bool ExecuteCommand(string commandName, string[] args)
        {
            bool foundCommand = false;
            foreach (var handler in CommandHandlers)
            {
                if (string.Compare(handler.Name, commandName, true) == 0)
                {
                    foundCommand = true;
                    if (handler.Execute(args))
                    {
                        EventManager.Instance.FireHideEvent();
                    }
                }
            }
            return foundCommand;
        }

        static public List<ICommand> Search(string text)
        {
            List<ICommand> retVal = new List<ICommand>();

            if (string.IsNullOrWhiteSpace(text) == false)
            {
                foreach (var handler in CommandHandlers)
                {
                    if (handler.Name.ToLower().StartsWith(text.ToLower()) == true)
                    {
                        retVal.Add(handler);
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
                            case "SHOW ON":
                                showCLipboard = true;
                                handled = true;
                                break;
                            case "SHOW OFF":
                                showCLipboard = false;
                                handled = true;
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
