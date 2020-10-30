using System.Threading.Tasks;
using System.Windows;
using ConferenceDude.Domain.Sessions;

namespace ConferenceDude.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SessionWindow
    {
        public SessionWindow()
        {
            InitializeComponent();
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
            var session = new Session();
            TextBoxId.Text = session.Id.ToString();
            TextBoxTitle.Text = session.Title;
            TextBoxAbstract.Text = session.Abstract;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var session = new Session
            {
                Id = int.Parse(TextBoxId.Text),
                Title = TextBoxTitle.Text,
                Abstract = TextBoxAbstract.Text
            };

            var validationResult = session.Validate();
            if (!validationResult.Success)
            {
                foreach (var message in validationResult.Messages)
                {
                    MessageBox.Show(
                        $"Feld {message.FieldName} - {message.ErrorMessage}");
                }

                return;
            }

            var apiClient = new ApiClient();
            var saved = await apiClient.Save(session);
            MessageBox.Show(saved ? 
                "Session erfolgreich gespeichert." : 
                "Session konnte nicht gespeichert werden.", "Info");
            await LoadSessions();
        }

        private async void Window_Initialized(object sender, System.EventArgs e)
        {
            await LoadSessions();
            NewSession();
        }

        private void DataGridSessions_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var session = (DataGridSessions.SelectedItem as SessionModel);
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
