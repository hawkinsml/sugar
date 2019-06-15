using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Helpers;

namespace Sugar.Components.Commands
{
    public class NewGuid : BaseCommand
    {
        public NewGuid()
        {
            Name = "Guid";
            Description = "Creates a new Guid value.";
           
            ParamList = new string[] { "Format" };
            ParamDescriptionList = new string[] { "" };
            ParamRequired = new bool[] { false };

            Help = "<h3>Guid</h3><p>" + Description + "</p>" +
                   "<dl><dt>Format code <span class='label label-default'>optional</span></dt>" +
                   "N -&gt; 00000000000000000000000000000000<br/>" +
                   "D -&gt; 00000000-0000-0000-0000-000000000000<br/>" +
                   "B -&gt; {00000000-0000-0000-0000-000000000000}<br/>" +
                   "P -&gt; (00000000-0000-0000-0000-000000000000)<br/>" +
                   "X -&gt; {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}<br/>";
        }

        static public void Init(ICommandManager commandManager)
        {
            commandManager.AddCommandHandler(new NewGuid());
        }

        override public bool Execute(string[] args)
        {
            Guid obj = Guid.NewGuid();
            string formatCode = "D";

            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                formatCode = args[1];
            }
            
            Clipboard.SetText(obj.ToString(formatCode), TextDataFormat.Text);

            return true; // hide command window
        }
    }
}
