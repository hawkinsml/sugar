using Sugar.Components;
namespace Sugar
{
    partial class CommandForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.commandTextBox = new Sugar.Components.CommandTextBox();
            this.tbShadow = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // commandTextBox
            // 
            this.commandTextBox.AcceptsReturn = true;
            this.commandTextBox.AcceptsTab = true;
            this.commandTextBox.BackColor = System.Drawing.Color.Black;
            this.commandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandTextBox.Font = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.commandTextBox.Location = new System.Drawing.Point(21, 24);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(272, 31);
            this.commandTextBox.TabIndex = 2;
            this.commandTextBox.TextChanged += new System.EventHandler(this.commandTextBox_TextChanged);
            this.commandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandTextBox_KeyDown);
            // 
            // tbShadow
            // 
            this.tbShadow.AcceptsReturn = true;
            this.tbShadow.AcceptsTab = true;
            this.tbShadow.BackColor = System.Drawing.Color.IndianRed;
            this.tbShadow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbShadow.Font = new System.Drawing.Font("Courier New", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbShadow.ForeColor = System.Drawing.Color.LightGray;
            this.tbShadow.Location = new System.Drawing.Point(43, 49);
            this.tbShadow.Name = "tbShadow";
            this.tbShadow.ReadOnly = true;
            this.tbShadow.Size = new System.Drawing.Size(272, 31);
            this.tbShadow.TabIndex = 1;
            // 
            // CommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.HotPink;
            this.BackgroundImage = global::Sugar.Properties.Resources.commandwindow;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(327, 92);
            this.ControlBox = false;
            this.Controls.Add(this.commandTextBox);
            this.Controls.Add(this.tbShadow);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "CommandForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sugar";
            this.TransparencyKey = System.Drawing.Color.HotPink;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.VisibleChanged += new System.EventHandler(this.CommandForm_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommandForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TextBox tbShadow;
        private CommandTextBox commandTextBox;
    }
}

