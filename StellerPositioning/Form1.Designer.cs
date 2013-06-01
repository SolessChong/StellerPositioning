namespace StellerPositioning
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.trackBarRA = new System.Windows.Forms.TrackBar();
            this.trackBarDec = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDec)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(24, 28);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(796, 507);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            this.imageBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox1_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 605);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(980, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // trackBarRA
            // 
            this.trackBarRA.Location = new System.Drawing.Point(24, 541);
            this.trackBarRA.Maximum = 240;
            this.trackBarRA.Name = "trackBarRA";
            this.trackBarRA.Size = new System.Drawing.Size(796, 45);
            this.trackBarRA.TabIndex = 4;
            this.trackBarRA.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // trackBarDec
            // 
            this.trackBarDec.Location = new System.Drawing.Point(842, 28);
            this.trackBarDec.Maximum = 90;
            this.trackBarDec.Name = "trackBarDec";
            this.trackBarDec.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarDec.Size = new System.Drawing.Size(45, 507);
            this.trackBarDec.TabIndex = 5;
            this.trackBarDec.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 627);
            this.Controls.Add(this.trackBarDec);
            this.Controls.Add(this.trackBarRA);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.imageBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TrackBar trackBarRA;
        private System.Windows.Forms.TrackBar trackBarDec;
    }
}

