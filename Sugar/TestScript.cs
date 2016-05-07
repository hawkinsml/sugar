using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Script
{
    public bool Execute(string[] args)
    {
        string text = Clipboard.GetText();
        if (!string.IsNullOrWhiteSpace(text))
        {
            Clipboard.SetText(text.ToUpper(), TextDataFormat.Text);
        }
        return true; 
    }
}