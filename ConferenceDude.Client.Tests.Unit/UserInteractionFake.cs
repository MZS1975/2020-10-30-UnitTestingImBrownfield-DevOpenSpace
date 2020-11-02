namespace ConferenceDude.UI.Tests.Unit
{
    using System.Collections.Generic;
    using Services;

    public class UserInteractionFake : IUserInteractionService
    {
        public List<string> Messages { get; } = new List<string>();
        
        public List<string> Errors { get; } = new List<string>();

        private bool _yesNoResponse;

        public void ConfigureYesNo(bool expectedResponse)
        {
            _yesNoResponse = expectedResponse;
        }

        public void ShowSuccess(string infoMessage)
        {
            Messages.Add(infoMessage);
        }

        public void ShowError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }

        public bool Confirm(string question)
        {
            return _yesNoResponse;
        }
    }
}
