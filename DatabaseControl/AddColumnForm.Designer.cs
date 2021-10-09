
namespace DatabaseControl
{
    partial class AddColumnForm
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
            this.Confirm = new System.Windows.Forms.Button();
            this.ColumnName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TypeVal = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Confirm
            // 
            this.Confirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.Confirm.Location = new System.Drawing.Point(98, 183);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(173, 37);
            this.Confirm.TabIndex = 5;
            this.Confirm.Text = "Confirm";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // ColumnName
            // 
            this.ColumnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.ColumnName.Location = new System.Drawing.Point(136, 80);
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.Size = new System.Drawing.Size(173, 32);
            this.ColumnName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(93, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "New Column";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label2.Location = new System.Drawing.Point(32, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label3.Location = new System.Drawing.Point(32, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Type";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TypeVal
            // 
            this.TypeVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.TypeVal.FormattingEnabled = true;
            this.TypeVal.Items.AddRange(new object[] {
            "System.Int32",
            "System.Single",
            "System.Char",
            "System.String",
            "System.CharInvl",
            "System.StringInvl"});
            this.TypeVal.Location = new System.Drawing.Point(136, 127);
            this.TypeVal.Name = "TypeVal";
            this.TypeVal.Size = new System.Drawing.Size(173, 34);
            this.TypeVal.TabIndex = 9;
            // 
            // AddColumnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 257);
            this.Controls.Add(this.TypeVal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.ColumnName);
            this.Controls.Add(this.label1);
            this.Name = "AddColumnForm";
            this.Text = "AddColumnForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.TextBox ColumnName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TypeVal;
    }
}