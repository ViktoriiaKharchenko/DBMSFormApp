
namespace DatabaseControl
{
    partial class JoinTablesForm
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
            this.Column1 = new System.Windows.Forms.ComboBox();
            this.Table1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Table2 = new System.Windows.Forms.ComboBox();
            this.Column2 = new System.Windows.Forms.ComboBox();
            this.Join = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Column1
            // 
            this.Column1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Column1.FormattingEnabled = true;
            this.Column1.Location = new System.Drawing.Point(319, 85);
            this.Column1.Name = "Column1";
            this.Column1.Size = new System.Drawing.Size(121, 28);
            this.Column1.TabIndex = 0;
            // 
            // Table1
            // 
            this.Table1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Table1.FormattingEnabled = true;
            this.Table1.Location = new System.Drawing.Point(91, 85);
            this.Table1.Name = "Table1";
            this.Table1.Size = new System.Drawing.Size(121, 28);
            this.Table1.TabIndex = 1;
            this.Table1.SelectedIndexChanged += new System.EventHandler(this.Table1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label1.Location = new System.Drawing.Point(182, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Join Tables";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Table 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label3.Location = new System.Drawing.Point(228, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Column";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label4.Location = new System.Drawing.Point(228, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Column";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label5.Location = new System.Drawing.Point(12, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "Table 2";
            // 
            // Table2
            // 
            this.Table2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Table2.FormattingEnabled = true;
            this.Table2.Location = new System.Drawing.Point(91, 147);
            this.Table2.Name = "Table2";
            this.Table2.Size = new System.Drawing.Size(121, 28);
            this.Table2.TabIndex = 6;
            this.Table2.SelectedIndexChanged += new System.EventHandler(this.Table2_SelectedIndexChanged);
            // 
            // Column2
            // 
            this.Column2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Column2.FormattingEnabled = true;
            this.Column2.Location = new System.Drawing.Point(319, 147);
            this.Column2.Name = "Column2";
            this.Column2.Size = new System.Drawing.Size(121, 28);
            this.Column2.TabIndex = 5;
            // 
            // Join
            // 
            this.Join.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Join.Location = new System.Drawing.Point(187, 203);
            this.Join.Name = "Join";
            this.Join.Size = new System.Drawing.Size(117, 47);
            this.Join.TabIndex = 9;
            this.Join.Text = "Join";
            this.Join.UseVisualStyleBackColor = true;
            this.Join.Click += new System.EventHandler(this.Join_Click);
            // 
            // JoinTablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 292);
            this.Controls.Add(this.Join);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Table2);
            this.Controls.Add(this.Column2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Table1);
            this.Controls.Add(this.Column1);
            this.Name = "JoinTablesForm";
            this.Text = "JoinTablesForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Column1;
        private System.Windows.Forms.ComboBox Table1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Table2;
        private System.Windows.Forms.ComboBox Column2;
        private System.Windows.Forms.Button Join;
    }
}