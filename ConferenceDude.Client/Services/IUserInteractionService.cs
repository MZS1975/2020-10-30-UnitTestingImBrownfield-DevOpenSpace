namespace ConferenceDude.UI.Services
{
    public interface IUserInteractionService
    {
        void ShowSuccess(string infoMessage);

        void ShowError(string errorMessage);
        
        bool Confirm(string question);
    }
}
