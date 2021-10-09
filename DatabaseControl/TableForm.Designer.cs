
namespace DatabaseControl
{
    partial class TableForm
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
            this.DeleteRow = new System.Windows.Forms.Button();
            this.AddRow = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // DeleteRow
            // 
            this.DeleteRow.Location = new System.Drawing.Point(247, 12);
            this.DeleteRow.Name = "DeleteRow";
            this.DeleteRow.Size = new System.Drawing.Size(109, 38);
            this.DeleteRow.TabIndex = 5;
            this.DeleteRow.Text = "DeleteRow";
            this.DeleteRow.UseVisualStyleBackColor = true;
            // 
            // AddRow
            // 
            this.AddRow.Location = new System.Drawing.Point(128, 12);
            this.AddRow.Name = "AddRow";
            this.AddRow.Size = new System.Drawing.Size(113, 38);
            this.AddRow.TabIndex = 4;
            this.AddRow.Text = "AddRow";
            this.AddRow.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(776, 382);
            this.dataGridView1.TabIndex = 3;
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(12, 12);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(113, 38);
            this.Back.TabIndex = 6;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.DeleteRow);
            this.Controls.Add(this.AddRow);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TableForm";
            this.Text = "TableForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DeleteRow;
        private System.Windows.Forms.Button AddRow;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Back;
    }
}