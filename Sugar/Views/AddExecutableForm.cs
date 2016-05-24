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
    public partial class AddExecutableForm : Form
    {
        public string Name { 
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public string Path { 
            get { return tbPath.Text; }
            set { tbPath.Text = value; }
        }

        public string Arguments { 
            get { return tbArguments.Text; }
            set { tbArguments.Text = value; }
        }

        public AddExecutableForm()
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
