using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Saritasa.JiraDayIssues.Domain.Issues.Dtos;
using Saritasa.JiraDayIssues.Domain.Issues.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saritasa.JiraDayIssues.WPF.ViewModel
{
    /// <summary>
    /// A class that stores logic for working with models.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly JiraService jiraService;
        private readonly MainWindow mainWindow = App.Current.MainWindow as MainWindow;

        private CancellationTokenSource cancelTokenSource;

        private string title = "JIRA Day Issues";

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        private string description = "The application allows you to view your day issues and their worklog from JIRA." +
            "To start select the date below. Then you can select issue to left to show worklogs.";

        public string Description
        {
            get => description;
            set => Set(ref description, value);
        }

        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get => selectedDate;
            set => Set(ref selectedDate, value);
        }

        private IssueDto selectedIssue;

        public IssueDto SelectedIssue
        {
            get => selectedIssue;
            set => Set(ref selectedIssue, value);
        }

        private ObservableCollection<IssueDto> issues;

        public ObservableCollection<IssueDto> Issues
        {
            get => issues;
            set => Set(ref issues, value);
        }

        private bool isVisibleLoadingPanel;

        public bool IsVisibleLoadingPanel
        {
            get => isVisibleLoadingPanel;
            set => Set(ref isVisibleLoadingPanel, value);
        }

        /// <summary>
        /// Command for asynchronous issues loading.
        /// </summary>
        public ICommand LoadIssuesAsyncCommand { get; }

        /// <summary>
        /// Command to cancel user request.
        /// </summary>
        public ICommand CancelRequestCommand { get; }

        /// <summary>
        /// <seealso cref="MainWindow" /> constructor class.
        /// </summary>
        public MainWindowViewModel()
        {
            jiraService = new JiraService(mainWindow.JiraLogin, mainWindow.JiraPassword);

            LoadIssuesAsyncCommand = new RelayCommand(async () => await OnLoadIssuesExecuteAsync());
            CancelRequestCommand = new RelayCommand(CancelRequestExecute);
        }

        /// <summary>
        /// Method to get issues from domain model.
        /// </summary>
        /// <returns></returns>
        private async Task OnLoadIssuesExecuteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancelTokenSource = new CancellationTokenSource();
            cancellationToken = cancelTokenSource.Token;

            IsVisibleLoadingPanel = true;

            try
            {
                Issues = new ObservableCollection<IssueDto>(await jiraService.GetIssuesAsync(mainWindow.JiraLogin, selectedDate, cancellationToken) as List<IssueDto>);
            }
            catch (TaskCanceledException)
            {
                return;
            }
            finally
            {
                IsVisibleLoadingPanel = false;
            }
        }

        /// <summary>
        /// Method to cancel user request.
        /// </summary>
        private void CancelRequestExecute()
        {
            cancelTokenSource.Cancel();
        }
    }
}