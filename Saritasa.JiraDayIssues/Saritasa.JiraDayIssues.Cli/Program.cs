using McMaster.Extensions.CommandLineUtils;
using Saritasa.JiraDayIssues.Domain.Issues.Reporters;
using Saritasa.JiraDayIssues.Domain.Issues.Services;
using System;
using System.Threading.Tasks;

namespace Saritasa.JiraDayIssues.Cli
{
    /// <summary>
    /// Program class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// App entry point.
        /// </summary>
        /// <param name="args">Input parameters.</param>
        static async Task Main(string[] args)
        {
            // Create command.
            var app = new CommandLineApplication();

            // Add a command-line options for command.
            var optionDate = app.Option("-d|--date=<DATE>", "Date", CommandOptionType.SingleOrNoValue);
            var optionUsername = app.Option("-u|--username=<EMAIL>", "Email", CommandOptionType.SingleValue);
            var optionPassword = app.Option("-p|--password=<PASSWORD>", "Password", CommandOptionType.SingleValue);

            DateTime date = DateTime.Now;
            string username = string.Empty;
            string password = string.Empty;

            // Invoke command.
            app.OnExecute(() =>
            {
                if (optionDate.HasValue())
                {
                    try
                    {
                        date = DateTime.Parse(optionDate.Value());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid date.");
                    }
                }
                else
                {
                    date = DateTime.Today;
                }

                username = optionUsername.HasValue()
                            ? optionUsername.Value()
                            : app.GetHelpText();

                password = optionPassword.HasValue()
                            ? optionPassword.Value()
                            : app.GetHelpText();
            });

            app.Execute(args);

            JiraService jiraService = new JiraService(username, password);
            JiraReporter reporter = new JiraReporter(jiraService);

            string report = await reporter.GetDailyReportAsync(username, date);

            Console.WriteLine(report);

            // Delay.
            Console.ReadKey();
        }
    }
}
