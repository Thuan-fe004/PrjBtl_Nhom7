namespace WindowsFormsApp1
{
    partial class frm_Client
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
            this.lvMassage = new System.Windows.Forms.ListView();
            this.tbMassage = new System.Windows.Forms.TextBox();
            this.btMassage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvMassage
            // 
            this.lvMassage.HideSelection = false;
            this.lvMassage.Location = new System.Drawing.Point(12, 12);
            this.lvMassage.Name = "lvMassage";
            this.lvMassage.Size = new System.Drawing.Size(776, 354);
            this.lvMassage.TabIndex = 0;
            this.lvMassage.UseCompatibleStateImageBehavior = false;
            this.lvMassage.View = System.Windows.Forms.View.List;
            this.lvMassage.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // tbMassage
            // 
            this.tbMassage.Location = new System.Drawing.Point(12, 372);
            this.tbMassage.Multiline = true;
            this.tbMassage.Name = "tbMassage";
            this.tbMassage.Size = new System.Drawing.Size(695, 66);
            this.tbMassage.TabIndex = 1;
            this.tbMassage.TextChanged += new System.EventHandler(this.tbMassage_TextChanged);
            // 
            // btMassage
            // 
            this.btMassage.Location = new System.Drawing.Point(713, 372);
            this.btMassage.Name = "btMassage";
            this.btMassage.Size = new System.Drawing.Size(75, 66);
            this.btMassage.TabIndex = 2;
            this.btMassage.Text = "Gửi ";
            this.btMassage.UseVisualStyleBackColor = true;
            this.btMassage.Click += new System.EventHandler(this.btMassage_Click);
            // 
            // frm_Client
            // 
            this.AcceptButton = this.btMassage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btMassage);
            this.Controls.Add(this.tbMassage);
            this.Controls.Add(this.lvMassage);
            this.Name = "frm_Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMassage;
        private System.Windows.Forms.TextBox tbMassage;
        private System.Windows.Forms.Button btMassage;
    }
}

