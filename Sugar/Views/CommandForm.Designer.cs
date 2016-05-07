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
            this.suggestedLabel = new System.Windows.Forms.Label();
            this.commandTextBox = new Sugar.Components.CommandTextBox();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // suggestedLabel
            // 
            this.suggestedLabel.AutoSize = true;
            this.suggestedLabel.BackColor = System.Drawing.Color.Transparent;
            this.suggestedLabel.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.suggestedLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.suggestedLabel.Location = new System.Drawing.Point(18, 5);
            this.suggestedLabel.Name = "suggestedLabel";
            this.suggestedLabel.Size = new System.Drawing.Size(0, 23);
            this.suggestedLabel.TabIndex = 3;
            // 
            // commandTextBox
            // 
            this.commandTextBox.AcceptsReturn = true;
            this.commandTextBox.AcceptsTab = true;
            this.commandTextBox.BackColor = System.Drawing.Color.White;
            this.commandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandTextBox.Font = new System.Drawing.Font("Verdana", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandTextBox.ForeColor = System.Drawing.Color.Black;
            this.commandTextBox.Location = new System.Drawing.Point(18, 35);
            this.commandTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.commandTextBox.MaximumSize = new System.Drawing.Size(418, 56);
            this.commandTextBox.MinimumSize = new System.Drawing.Size(418, 56);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(418, 56);
            this.commandTextBox.TabIndex = 2;
            this.commandTextBox.TextChanged += new System.EventHandler(this.commandTextBox_TextChanged);
            this.commandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandTextBox_KeyDown);
            // 
            // CommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.HotPink;
            this.BackgroundImage = global::Sugar.Properties.Resources.commandwindow;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(456, 100);
            this.ControlBox = false;
            this.Controls.Add(this.suggestedLabel);
            this.Controls.Add(this.commandTextBox);
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
        private CommandTextBox commandTextBox;
        private System.Windows.Forms.Label suggestedLabel;
    }
}

