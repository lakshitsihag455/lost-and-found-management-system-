using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LostAndFoundApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // If login.html is available in the app output folder, open it in the default browser
            // and exit. This makes the app show the HTML login page in the browser when you run the
            // project from Visual Studio.
            string htmlFile = Path.Combine(AppContext.BaseDirectory, "login.html");
            if (File.Exists(htmlFile))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(htmlFile) { UseShellExecute = true });
                    return; // exit application after opening browser
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to open browser: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // fall through to show WinForms fallback if needed
                }
            }

            // Fallback: run the WinForms login if HTML isn't available or opening failed
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
