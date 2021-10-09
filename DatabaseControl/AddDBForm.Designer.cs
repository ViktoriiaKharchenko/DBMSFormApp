
namespace DatabaseControl
{
    partial class AddDBForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.DbName = new System.Windows.Forms.TextBox();
            this.Confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(68, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Database Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DbName
            // 
            this.DbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.DbName.Location = new System.Drawing.Point(103, 69);
            this.DbName.Name = "DbName";
            this.DbName.Size = new System.Drawing.Size(173, 32);
            this.DbName.TabIndex = 1;
            // 
            // Confirm
            // 
            this.Confirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.Confirm.Location = new System.Drawing.Point(103, 107);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(173, 37);
            this.Confirm.TabIndex = 2;
            this.Confirm.Text = "Confirm";
            this.Confirm.UseVisualStyleBackColor = true;
            // 
            // AddDBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 188);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.DbName);
            this.Controls.Add(this.label1);
            this.Name = "AddDBForm";
            this.Text = "AddDBForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DbName;
        private System.Windows.Forms.Button Confirm;
    }
}