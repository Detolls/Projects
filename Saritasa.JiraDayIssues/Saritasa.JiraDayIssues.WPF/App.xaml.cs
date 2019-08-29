using McMaster.Extensions.CommandLineUtils;
using System.Windows;

namespace Saritasa.JiraDayIssues.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// WPF App entry point.
        /// </summary>
        /// <param name="e">Args.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Create command.
            var app = new CommandLineApplication();

            // Add a command-line options for command.
            var optionUsername = app.Option("-u|--username=<EMAIL>", "Email", CommandOptionType.SingleValue);
            var optionPassword = app.Option("-p|--password=<PASSWORD>", "Password", CommandOptionType.SingleValue);

            string username = string.Empty;
            string password = string.Empty;

            // Invoke command.
            app.OnExecute(() =>
            {
                if (!optionUsername.HasValue())
                {
                    MessageBox.Show(app.GetHelpText());
                }
                else
                {
                    username = optionUsername.Value();
                }

                if (!optionPassword.HasValue())
                {
                    MessageBox.Show(app.GetHelpText());
                }
                else
                {
                    password = optionPassword.Value();
                }
            });

            app.Execute(e.Args);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                MainWindow mainWindow = new MainWindow(username, password);
                mainWindow.Show();
            }
            else
            {
                App.Current.Shutdown();
            }
        }
    }
}
