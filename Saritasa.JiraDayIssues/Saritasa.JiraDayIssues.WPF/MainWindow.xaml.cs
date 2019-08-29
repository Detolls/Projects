using System;
using System.Windows;

namespace Saritasa.JiraDayIssues.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// JIRA login.
        /// </summary>
        public string JiraLogin { get; }

        /// <summary>
        /// JIRA password.
        /// </summary>
        public string JiraPassword { get; }

        /// <summary>
        /// <seealso cref="MainWindow"/> constructor class.
        /// </summary>
        /// <param name="username">JIRA username.</param>
        /// <param name="password">JIRA password.</param>
        public MainWindow(string username, string password)
        {
            JiraLogin = username ?? throw new ArgumentNullException("Username should not be null.");
            JiraPassword = password ?? throw new ArgumentNullException("Password should not be null.");

            InitializeComponent();

            datePicker.SelectedDate = DateTime.Today;
        }
    }
}
