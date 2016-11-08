namespace Homework_07
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
            this.pbxGraph = new System.Windows.Forms.PictureBox();
            this.bgwDrawing = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxGraph
            // 
            this.pbxGraph.BackColor = System.Drawing.Color.White;
            this.pbxGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxGraph.Location = new System.Drawing.Point(12, 12);
            this.pbxGraph.Name = "pbxGraph";
            this.pbxGraph.Size = new System.Drawing.Size(480, 450);
            this.pbxGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxGraph.TabIndex = 5;
            this.pbxGraph.TabStop = false;
            this.pbxGraph.Click += new System.EventHandler(this.pbxGraph_Click);
            // 
            // bgwDrawing
            // 
            this.bgwDrawing.WorkerReportsProgress = true;
            this.bgwDrawing.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwDrawing_DoWork);
            this.bgwDrawing.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwDrawing_Done);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 474);
            this.Controls.Add(this.pbxGraph);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Cubic Spline";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbxGraph)).EndInit();
            this.ResumeLayout(false);

        }

        private void PbxGraph_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void BgwDrawing_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void BgwDrawing_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.PictureBox pbxGraph;
        private System.ComponentModel.BackgroundWorker bgwDrawing;
    }
}

