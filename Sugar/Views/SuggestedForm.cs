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
    public partial class SuggestedForm : Form
    {
        public SuggestedForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public void SetSuggestions(List<string> list)
        {
            listBox.Items.Clear();

            foreach( var item in list )
            {
                listBox.Items.Add(item);
            }
        }
    }
}
