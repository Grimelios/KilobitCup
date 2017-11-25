using System;

namespace KilobitCup
{
	/// <summary>
	/// Main program for the bit cup.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// Client ID for the application.
		/// </summary>
		public const string ClientID = "zulz2i7hm8u5vofu2095940hrq81nx";

		/// <summary>
		/// Main function for the program.
		/// </summary>
		[STAThread]
        public static void Main()
        {
	        using (MainGame game = new MainGame())
	        {
				game.Run();
			}
        }
    }
}
