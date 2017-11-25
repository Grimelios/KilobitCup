using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KilobitCup.Twitch;

namespace KilobitCup.Forms
{
	/// <summary>
	/// Form used to allow users to authorize the program.
	/// </summary>
	public partial class AuthorizationForm : Form
	{
		/// <summary>
		/// Constructs the form.
		/// </summary>
		public AuthorizationForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Access token to the user's channel.
		/// </summary>
		public string AccessToken { get; private set; }

		/// <summary>
		/// Called when the form has loaded.
		/// </summary>
		private void AuthorizationForm_Load(object sender, EventArgs e)
		{
			string url = "https://api.twitch.tv/kraken/oauth2/authorize" +
				"?response_type=token" +
				$"&client_id={Program.ClientID}" +
				"&redirect_uri=http://localhost" +
				"&scope=user_read";

			webBrowser.Navigate(url);
		}

		/// <summary>
		/// Called when the browser navigates to a new site.
		/// </summary>
		private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			string url = e.Url.ToString();

			// The redirect URL is localhost, so other pages can be ignored.
			if (!url.StartsWith("http://localhost"))
			{
				return;
			}

			// "access_token=" = 13 characters.
			int tokenIndex = url.IndexOf('#') + 14;
			int length = url.IndexOf('&', tokenIndex) - tokenIndex;

			AccessToken = url.Substring(tokenIndex, length);
			DialogResult = DialogResult.OK;
		}
	}
}
