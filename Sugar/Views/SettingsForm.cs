using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sugar.Components.Settings;

namespace Sugar.Views
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            ScriptCommandList.View = View.Details;
            ScriptCommandList.GridLines = true;
            ScriptCommandList.FullRowSelect = true;
            ScriptCommandList.Columns.Add("Script Command Name", 200);
            ScriptCommandList.Columns.Add("Help", 200);
            ScriptCommandList.Columns.Add("Code", 200);

            ExecutablesList.View = View.Details;
            ExecutablesList.GridLines = true;
            ExecutablesList.FullRowSelect = true;
            ExecutablesList.Columns.Add("Executable Name", 200);
            ExecutablesList.Columns.Add("Path", 200);
            ExecutablesList.Columns.Add("Arguments", 100);


            SettingsModal data = SettingsManager.LoadSettings();

            foreach (var item in data.Commands.OrderBy( o => o.Name ) )
            {
                string[] arr = new string[3];
                arr[0] = item.Name;
                arr[1] = item.Help;
                arr[2] = item.SourceCode;
                ListViewItem itm = new ListViewItem(arr);
                ScriptCommandList.Items.Add(itm);
            }

            foreach (var item in data.Executables.OrderBy(o => o.Name))
            {
                string[] arr = new string[3];
                arr[0] = item.Name;
                arr[1] = item.Path;
                arr[2] = item.Arguments;
                ListViewItem itm = new ListViewItem(arr);
                ExecutablesList.Items.Add(itm);
            }
        }

        private void ExecutablesList_DoubleClick(object sender, EventArgs e)
        {
            AddExecutableForm form = new AddExecutableForm();
            form.Name = ExecutablesList.SelectedItems[0].SubItems[0].Text;
            form.Path = ExecutablesList.SelectedItems[0].SubItems[1].Text;
            form.Arguments = ExecutablesList.SelectedItems[0].SubItems[2].Text;

            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ExecutablesList.SelectedItems[0].SubItems[0].Text = form.Name;
                ExecutablesList.SelectedItems[0].SubItems[1].Text = form.Path;
                ExecutablesList.SelectedItems[0].SubItems[2].Text = form.Arguments;
            }
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            // Save settings.

            SettingsModal data = new SettingsModal();
            foreach (ListViewItem  item in ScriptCommandList.Items)
            {
                CommandModel cmd = new CommandModel();
                cmd.Name = item.SubItems[0].Text;
                cmd.Help = item.SubItems[1].Text;
                cmd.SourceCode = item.SubItems[2].Text;
                data.Commands.Add(cmd);
            }

            foreach (ListViewItem item in ExecutablesList.Items)
            {
                ExecutableModel exe = new ExecutableModel();
                exe.Name = item.SubItems[0].Text;
                exe.Path = item.SubItems[1].Text;
                exe.Arguments = item.SubItems[2].Text;
                data.Executables.Add(exe);
            }

            SettingsManager.SaveSettings(data);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ScriptCommandList_DoubleClick(object sender, EventArgs e)
        {
            AddScriptCmdForm form = new AddScriptCmdForm();
            form.Name = ScriptCommandList.SelectedItems[0].SubItems[0].Text;
            form.Help = ScriptCommandList.SelectedItems[0].SubItems[1].Text;
            form.SourceCode = ScriptCommandList.SelectedItems[0].SubItems[2].Text;

            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ScriptCommandList.SelectedItems[0].SubItems[0].Text = form.Name;
                ScriptCommandList.SelectedItems[0].SubItems[1].Text = form.Help;
                ScriptCommandList.SelectedItems[0].SubItems[2].Text = form.SourceCode;
            }
        }

        private void AddCmdBtn_Click(object sender, EventArgs e)
        {
            AddScriptCmdForm form = new AddScriptCmdForm();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("");
            sb.AppendLine("public class Script");
            sb.AppendLine("{");
            sb.AppendLine("    public bool Execute(string[] args, string clipboardText, out string resultText)");
            sb.AppendLine("    {");
            sb.AppendLine("        resultText = \"I changed the clipboard.  + clipboardText;");
            sb.AppendLine("        return true;");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            form.Name = "My New Command";
            form.Help = "<h3>My New Command</h3><p>Desription of My New Command</p>";
            form.SourceCode = sb.ToString();



            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string[] arr = new string[3];
                arr[0] = form.Name;
                arr[1] = form.Help;
                arr[2] = form.SourceCode;
                ListViewItem itm = new ListViewItem(arr);
                ScriptCommandList.Items.Add(itm);
            }
        }

        private void AddExeBtn_Click(object sender, EventArgs e)
        {
            AddExecutableForm form = new AddExecutableForm();

            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string[] arr = new string[3];
                arr[0] = form.Name;
                arr[1] = form.Path;
                arr[2] = form.Arguments;
                ListViewItem itm = new ListViewItem(arr);
                ExecutablesList.Items.Add(itm);
            }
        }
    }
}
