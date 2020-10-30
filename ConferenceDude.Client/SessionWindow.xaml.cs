using System.Threading.Tasks;
using System.Windows;

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
            TextBoxId.Text = "0";
            TextBoxTitle.Text = string.Empty;
            TextBoxAbstract.Text = string.Empty;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxTitle.Text))
            {
                MessageBox.Show("Es muss ein Titel angegeben werden.");
                return;
            }

            if (string.IsNullOrEmpty(TextBoxAbstract.Text))
            {
                MessageBox.Show("Es muss ein Abstract angegeben werden.");
                return;
            }

            if (int.TryParse(TextBoxId.Text, out var id))
            {
                var session = new Session
                {
                    Id = id,
                    Title = TextBoxTitle.Text,
                    Abstract = TextBoxAbstract.Text
                };

                var apiClient = new ApiClient();
                var saved = await apiClient.Save(session);
                MessageBox.Show(saved ? 
                    "Session erfolgreich gespeichert." : 
                    "Session konnte nicht gespeichert werden.", "Info");
                await LoadSessions();
            }
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
