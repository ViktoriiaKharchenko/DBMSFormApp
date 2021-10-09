
namespace DatabaseControl
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.AddRow = new System.Windows.Forms.Button();
            this.DeleteRow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBSystem_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddDB_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(250, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DeleteDB_Click);
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(12, 12);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(113, 38);
            this.Back.TabIndex = 7;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            // 
            // AddRow
            // 
            this.AddRow.Location = new System.Drawing.Point(369, 12);
            this.AddRow.Name = "AddRow";
            this.AddRow.Size = new System.Drawing.Size(113, 38);
            this.AddRow.TabIndex = 8;
            this.AddRow.Text = "AddRow";
            this.AddRow.UseVisualStyleBackColor = true;
            this.AddRow.Click += new System.EventHandler(this.AddRow_Click);
            // 
            // DeleteRow
            // 
            this.DeleteRow.Location = new System.Drawing.Point(488, 12);
            this.DeleteRow.Name = "DeleteRow";
            this.DeleteRow.Size = new System.Drawing.Size(113, 38);
            this.DeleteRow.TabIndex = 9;
            this.DeleteRow.Text = "DeleteRow";
            this.DeleteRow.UseVisualStyleBackColor = true;
            this.DeleteRow.Click += new System.EventHandler(this.DeleteRow_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DeleteRow);
            this.Controls.Add(this.AddRow);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button AddRow;
        private System.Windows.Forms.Button DeleteRow;
    }
}

