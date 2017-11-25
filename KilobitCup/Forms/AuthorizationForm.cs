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
		/// Called when the form has loaded.
		/// </summary>
		private void AuthorizationForm_Load(object sender, EventArgs e)
		{
			string url = "https://api.twitch.tv/kraken/oauth2/authorize" +
				"?response_type=token" +
				$"&client_id={TwitchAPI.ClientID}" +
				"&redirect_uri=http://localhost" +
				"&scope=user_read";

			webBrowser.Navigate(url);
		}
	}
}
