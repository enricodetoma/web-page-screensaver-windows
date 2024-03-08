namespace Miceli.Web_Page_Screensaver
{
    partial class ScreensaverForm
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
            this.closeButton = new System.Windows.Forms.Button();
            this.webView1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.transparentPanel1 = new TransparentPanel();
            ((System.ComponentModel.ISupportInitialize)(this.webView1)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(323, 15);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 28);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Visible = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // webView1
            // 
            this.webView1.CreationProperties = null;
            this.webView1.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView1.Location = new System.Drawing.Point(0, 0);
            this.webView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.webView1.MinimumSize = new System.Drawing.Size(27, 25);
            this.webView1.Name = "webView1";
            this.webView1.Size = new System.Drawing.Size(379, 322);
            this.webView1.TabIndex = 2;
            this.webView1.ZoomFactor = 1D;
            // 
            // transparentPanel1
            // 
            this.transparentPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transparentPanel1.Location = new System.Drawing.Point(0, 0);
            this.transparentPanel1.Name = "transparentPanel1";
            this.transparentPanel1.Size = new System.Drawing.Size(379, 322);
            this.transparentPanel1.TabIndex = 3;
            // 
            // ScreensaverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(379, 322);
            this.Controls.Add(this.transparentPanel1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.webView1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScreensaverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ScreensaverForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webView1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button closeButton;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView1;
        private TransparentPanel transparentPanel1;
    }
}

