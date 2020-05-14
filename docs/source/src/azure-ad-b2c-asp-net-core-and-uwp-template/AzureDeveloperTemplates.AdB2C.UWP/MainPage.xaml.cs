using AzureDeveloperTemplates.AdB2C.UWP.Services;
using AzureDeveloperTemplates.AdB2C.UWP.Services.Interfaces;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AzureDeveloperTemplates.AdB2C.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IAuthenticationService _authenticationService;
        public MainPage()
        {
            this.InitializeComponent();
            _authenticationService = new AuthenticationService();
        }

        private async void SignInButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SignInButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            SignInProgressRing.IsActive = true;

            var authenticationResult = await _authenticationService.Authenticate();

            SignInButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            SignInProgressRing.IsActive = false;

            if (authenticationResult != null)
            {
                MessageDialog dialog = new MessageDialog($"Token: {authenticationResult.AccessToken}");
                await dialog.ShowAsync();
            }
        }
    }
}
