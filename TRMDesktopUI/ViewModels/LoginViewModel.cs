using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Helpers;

namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
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

        // Enable Log In button only if UserName and Password fields are not empty
        public bool CanLogIn => (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password));

        /// <summary>
        /// authenticates the User
        /// </summary>
        /// <returns> Access_Token, UserName </returns>
        public async Task LogIn()
        {
            try
            {
                var result = await _apiHelper.Authenticate(UserName, Password);

                Console.WriteLine($"Access_Token: {result.Access_Token}");
                Console.WriteLine($"UserName: {result.UserName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
