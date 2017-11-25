namespace KilobitCup.Forms
{
	partial class AuthorizationForm
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
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// webBrowser
			// 
			this.webBrowser.Location = new System.Drawing.Point(8, 8);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.ScrollBarsEnabled = false;
			this.webBrowser.Size = new System.Drawing.Size(363, 443);
			this.webBrowser.TabIndex = 0;
			this.webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser_Navigated);
			// 
			// AuthorizationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(379, 459);
			this.Controls.Add(this.webBrowser);
			this.Name = "AuthorizationForm";
			this.Text = "AuthorizationForm";
			this.Load += new System.EventHandler(this.AuthorizationForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser;
	}
}