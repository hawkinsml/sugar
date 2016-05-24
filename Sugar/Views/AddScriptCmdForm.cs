using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sugar.Views
{
    public partial class AddScriptCmdForm : Form
    {
        public string Name { 
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public string Help { 
            get { return tbHelp.Text; }
            set { tbHelp.Text = value; }
        }

        public string SourceCode { 
            get { return tbScoureCode.Text; }
            set { tbScoureCode.Text = value; }
        }

        public AddScriptCmdForm()
        {
            InitializeComponent();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
