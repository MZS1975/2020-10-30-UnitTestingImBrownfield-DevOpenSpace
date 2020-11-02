namespace ConferenceDude.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SessionWindow
    {
        private readonly SessionsViewModel _sessionsViewModel;

        public SessionWindow(SessionsViewModel sessionsViewModel)
        {
            _sessionsViewModel = sessionsViewModel;
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, System.EventArgs e)
        {
            DataContext = _sessionsViewModel;
            await _sessionsViewModel.FillSessions();
        }
    }
}
