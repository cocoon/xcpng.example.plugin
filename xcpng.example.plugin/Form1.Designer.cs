namespace xcpng.example.plugin
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        private void InitializeComponent()
        {
            this.t1 = new System.Windows.Forms.TextBox();
            this.buttonGetRecords = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // t1
            // 
            this.t1.Location = new System.Drawing.Point(12, 51);
            this.t1.Multiline = true;
            this.t1.Name = "t1";
            this.t1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.t1.Size = new System.Drawing.Size(776, 387);
            this.t1.TabIndex = 0;
            // 
            // buttonGetRecords
            // 
            this.buttonGetRecords.Location = new System.Drawing.Point(13, 13);
            this.buttonGetRecords.Name = "buttonGetRecords";
            this.buttonGetRecords.Size = new System.Drawing.Size(75, 23);
            this.buttonGetRecords.TabIndex = 1;
            this.buttonGetRecords.Text = "Get Records";
            this.buttonGetRecords.UseVisualStyleBackColor = true;
            this.buttonGetRecords.Click += new System.EventHandler(this.buttonGetRecords_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(175, 12);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(75, 23);
            this.buttonLogout.TabIndex = 2;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(94, 12);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.buttonGetRecords);
            this.Controls.Add(this.t1);
            this.Name = "Form1";
            this.Text = "XCP-NG-Center Example Plugin 2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox t1;
        private System.Windows.Forms.Button buttonGetRecords;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonClear;
    }
}

