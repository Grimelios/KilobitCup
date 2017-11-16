using System;

namespace KilobitCup
{
	/// <summary>
	/// Main program for the bit cup.
	/// </summary>
    public static class Program
    {
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
