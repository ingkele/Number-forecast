namespace 号码预测器
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开始同步ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.hm1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hm2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hm3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hm4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hm5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hm6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hm7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始同步ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(853, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开始同步ToolStripMenuItem
            // 
            this.开始同步ToolStripMenuItem.Name = "开始同步ToolStripMenuItem";
            this.开始同步ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开始同步ToolStripMenuItem.Text = "开始同步";
            this.开始同步ToolStripMenuItem.Click += new System.EventHandler(this.开始同步ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 425);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "同步情况";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(228, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(625, 425);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "同步列表";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 17);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(222, 405);
            this.textBox1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hm1,
            this.hm2,
            this.hm3,
            this.hm4,
            this.hm5,
            this.hm6,
            this.hm7});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(619, 405);
            this.dataGridView1.TabIndex = 0;
            // 
            // hm1
            // 
            this.hm1.HeaderText = "号码1";
            this.hm1.Name = "hm1";
            // 
            // hm2
            // 
            this.hm2.HeaderText = "号码2";
            this.hm2.Name = "hm2";
            // 
            // hm3
            // 
            this.hm3.HeaderText = "号码3";
            this.hm3.Name = "hm3";
            // 
            // hm4
            // 
            this.hm4.HeaderText = "号码4";
            this.hm4.Name = "hm4";
            // 
            // hm5
            // 
            this.hm5.HeaderText = "号码5";
            this.hm5.Name = "hm5";
            // 
            // hm6
            // 
            this.hm6.HeaderText = "号码6";
            this.hm6.Name = "hm6";
            // 
            // hm7
            // 
            this.hm7.HeaderText = "号码7";
            this.hm7.Name = "hm7";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开始同步ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn hm1;
        private System.Windows.Forms.DataGridViewTextBoxColumn hm2;
        private System.Windows.Forms.DataGridViewTextBoxColumn hm3;
        private System.Windows.Forms.DataGridViewTextBoxColumn hm4;
        private System.Windows.Forms.DataGridViewTextBoxColumn hm5;
        private System.Windows.Forms.DataGridViewTextBoxColumn hm6;
        private System.Windows.Forms.DataGridViewTextBoxColumn hm7;
    }
}

