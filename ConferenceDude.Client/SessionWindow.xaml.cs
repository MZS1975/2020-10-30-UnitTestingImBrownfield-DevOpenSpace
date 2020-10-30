using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ConferenceDude.Domain;

namespace ConferenceDude.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SessionWindow
    {
        SessionValidator _validator;

        public SessionWindow()
        {
            InitializeComponent();
            _validator = new SessionValidator();

//#if DEBUG
            if (EventWaitHandle.TryOpenExisting("DUDECLIENT_PROCESS_READY", out var handle))
                handle.Set();
//#endif

        }

        private async Task LoadSessions()
        {
            var apiClient = new ApiClient();
            var sessions = await apiClient.AllSessions();
            DataGridSessions.ItemsSource = sessions;
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            NewSession();
        }

        private void NewSession()
        {
            TextBoxId.Text = "0";
            TextBoxTitle.Text = string.Empty;
            TextBoxAbstract.Text = string.Empty;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TextBoxId.Text, out var id)) return;
            var session = new Session
            {
                Id = id,
                Title = TextBoxTitle.Text,
                Abstract = TextBoxAbstract.Text
            };

            if (!_validator.IsValid(session, out var msg))
            {
                MessageBox.Show(msg);
                return;
            }

            var apiClient = new ApiClient();
            var saved = await apiClient.Save(session);
            MessageBox.Show(saved ? "Session erfolgreich gespeichert." : "Session konnte nicht gespeichert werden.",
                "Info");
            await LoadSessions();
        }


        private async void Window_Initialized(object sender, System.EventArgs e)
        {
            await LoadSessions();
            NewSession();
        }

        private void DataGridSessions_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var session = (DataGridSessions.SelectedItem as Session);
            if (session != null)
            {
                TextBoxId.Text = session.Id.ToString();
                TextBoxTitle.Text = session.Title;
                TextBoxAbstract.Text = session.Abstract;
            }
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Soll die Session wirklich gelöscht werden?", 
                "Bestätigen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (int.TryParse(TextBoxId.Text, out var id))
                {
                    var apiClient = new ApiClient();
                    var deleted = await apiClient.DeleteSession(id);
                    MessageBox.Show(deleted ? 
                        "Session erfolgreich gelöscht." : 
                        "Session konnte nicht gelöscht werden.", "Info");
                    NewSession();
                    await LoadSessions();
                }
            }
        }
    }
}
