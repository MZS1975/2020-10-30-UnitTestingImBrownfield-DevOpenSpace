namespace ConferenceDude.UI
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Application.Conference;
    using Commands;
    using Domain.Session.Policies;
    using JetBrains.Annotations;
    using Services;

    public class SessionsViewModel : INotifyPropertyChanged
    {
        private readonly IUserInteractionService _userInteractionService;
        private readonly IConferencePlanningService _conferencePlanningService;
        private SessionVewModel? _currentSession;

        public SessionsViewModel(IUserInteractionService userInteractionService, IConferencePlanningService conferencePlanningService)
        {
            _userInteractionService = userInteractionService;
            _conferencePlanningService = conferencePlanningService;
            NewSessionCommand = new RelayCommand(o => CreateNewSession());
            SaveSessionCommand = new RelayCommand(async o => await SaveSession(), o => CurrentSession != null);
            RemoveSessionCommand = new RelayCommand(async o => await RemoveSession(), o => CurrentSession != null);
            Sessions = new ObservableCollection<SessionVewModel>();
        }

        public ObservableCollection<SessionVewModel> Sessions { get; }

        public SessionVewModel? CurrentSession
        {
            get => _currentSession;
            set
            {
                if (Equals(value, _currentSession)) return;

                _currentSession = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewSessionCommand { get; }

        public ICommand SaveSessionCommand { get; }

        public ICommand RemoveSessionCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task FillSessions()
        {
            var sessions = await _conferencePlanningService.SessionPlanner.ListSessionsAsync();

            Sessions.Clear();
            foreach (var session in sessions)
            {
                Sessions.Add(new SessionVewModel(session));
            }
            
            CreateNewSession();
        }

        private void CreateNewSession()
        {
            var newSession = new SessionVewModel();
            Sessions.Add(newSession);
            CurrentSession = newSession;
        }

        private async Task SaveSession()
        {
            try
            {
                var session = CurrentSession!.ToSession();
                var result = await _conferencePlanningService.SessionPlanner.StoreSessionAsync(session);
                if (result.IsValid)
                {
                    _userInteractionService.ShowSuccess("Session successfully saved.");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (SessionPolicy violation in result.Violations!)
                    {
                        switch (violation)
                        {
                            case SessionPolicy.UniqueTitle:
                                sb.Append($"Title {session.Title} is not unique.");
                                break;
                        }
                    }

                    _userInteractionService.ShowError($"Session is not valid. Because: {sb}");
                }
            }
            catch
            {
                _userInteractionService.ShowError("Session could not be saved.");
            }

            await FillSessions();
        }

        private async Task RemoveSession()
        {
            if (_userInteractionService.Confirm("Do you really want to remove the session?"))
            {
                try
                {
                    await _conferencePlanningService.SessionPlanner.RemoveSessionAsync(CurrentSession!.ToSession().Id);
                    _userInteractionService.ShowSuccess("Session successfully removed.");

                }
                catch
                {
                    _userInteractionService.ShowError("Session could not be removed.");
                }

                await FillSessions();
            }
        }
    }
}
