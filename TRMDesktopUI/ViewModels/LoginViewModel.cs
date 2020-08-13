using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Helpers;

namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;
        private IEventAggregator _events;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

        /// <summary>
        /// gets the UserName from the ui user control - sets the UserName 
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        /// <summary>
        /// gets the Password from the ui user control - sets the Password 
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(ErrorMessage))
                {
                    output = true;
                }

                return output;
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        // Enable LogIn button only if UserName and Password fields are not empty
        public bool CanLogIn => (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password));

        /// <summary>
        /// Authenticates the User
        /// </summary>
        /// <returns> Access_Token, UserName </returns>
        public async Task LogIn()
        {
            try
            {
                // Resets the ErrorMessage to an empty string
                ErrorMessage = "";

                // User authentication
                var result = await _apiHelper.Authenticate(UserName, Password);

                // Capture more information about the user
                await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                // Publishing an event on UIThread
                _events.PublishOnUIThread(new LogOnEvent());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
