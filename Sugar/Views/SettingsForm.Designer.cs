namespace Sugar.Views
{
    partial class SettingsForm
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
            this.ExecutablesList = new System.Windows.Forms.ListView();
            this.ScriptCommandList = new System.Windows.Forms.ListView();
            this.doneBtn = new System.Windows.Forms.Button();
            this.AddCmdBtn = new System.Windows.Forms.Button();
            this.AddExeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExecutablesList
            // 
            this.ExecutablesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExecutablesList.Location = new System.Drawing.Point(12, 230);
            this.ExecutablesList.Name = "ExecutablesList";
            this.ExecutablesList.Size = new System.Drawing.Size(513, 183);
            this.ExecutablesList.TabIndex = 0;
            this.ExecutablesList.UseCompatibleStateImageBehavior = false;
            this.ExecutablesList.DoubleClick += new System.EventHandler(this.ExecutablesList_DoubleClick);
            // 
            // ScriptCommandList
            // 
            this.ScriptCommandList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScriptCommandList.Location = new System.Drawing.Point(12, 12);
            this.ScriptCommandList.Name = "ScriptCommandList";
            this.ScriptCommandList.Size = new System.Drawing.Size(513, 183);
            this.ScriptCommandList.TabIndex = 1;
            this.ScriptCommandList.UseCompatibleStateImageBehavior = false;
            this.ScriptCommandList.DoubleClick += new System.EventHandler(this.ScriptCommandList_DoubleClick);
            // 
            // doneBtn
            // 
            this.doneBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.doneBtn.Location = new System.Drawing.Point(450, 469);
            this.doneBtn.Name = "doneBtn";
            this.doneBtn.Size = new System.Drawing.Size(75, 23);
            this.doneBtn.TabIndex = 2;
            this.doneBtn.Text = "Done";
            this.doneBtn.UseVisualStyleBackColor = true;
            this.doneBtn.Click += new System.EventHandler(this.doneBtn_Click);
            // 
            // AddCmdBtn
            // 
            this.AddCmdBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddCmdBtn.Location = new System.Drawing.Point(450, 201);
            this.AddCmdBtn.Name = "AddCmdBtn";
            this.AddCmdBtn.Size = new System.Drawing.Size(75, 23);
            this.AddCmdBtn.TabIndex = 3;
            this.AddCmdBtn.Text = "Add";
            this.AddCmdBtn.UseVisualStyleBackColor = true;
            this.AddCmdBtn.Click += new System.EventHandler(this.AddCmdBtn_Click);
            // 
            // AddExeBtn
            // 
            this.AddExeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddExeBtn.Location = new System.Drawing.Point(450, 419);
            this.AddExeBtn.Name = "AddExeBtn";
            this.AddExeBtn.Size = new System.Drawing.Size(75, 23);
            this.AddExeBtn.TabIndex = 4;
            this.AddExeBtn.Text = "Add";
            this.AddExeBtn.UseVisualStyleBackColor = true;
            this.AddExeBtn.Click += new System.EventHandler(this.AddExeBtn_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.doneBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 504);
            this.ControlBox = false;
            this.Controls.Add(this.AddExeBtn);
            this.Controls.Add(this.AddCmdBtn);
            this.Controls.Add(this.doneBtn);
            this.Controls.Add(this.ScriptCommandList);
            this.Controls.Add(this.ExecutablesList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ExecutablesList;
        private System.Windows.Forms.ListView ScriptCommandList;
        private System.Windows.Forms.Button doneBtn;
        private System.Windows.Forms.Button AddCmdBtn;
        private System.Windows.Forms.Button AddExeBtn;
    }
}