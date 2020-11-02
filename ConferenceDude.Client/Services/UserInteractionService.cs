namespace ConferenceDude.UI.Services
{
    using System.Windows;

    public class UserInteractionService : IUserInteractionService
    {
        public void ShowSuccess(string infoMessage)
        {
            MessageBox.Show(infoMessage, "Success");
        }

        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error");
        }

        public bool Confirm(string question)
        {
            return MessageBox.Show(question, "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}
