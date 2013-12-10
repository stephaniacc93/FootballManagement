using FootballManagement.Client.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FootballManagement.Client.FootballManagementServiceReference;
using FootballManagement.Client.Views.Team_Pages;
using FootballManagement.Client.Views.Referee_and_Player_Pages;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FootballManagement.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void BTTNTorneos_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TournamentMenuPage));
        }

        private void BTTNTeams_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TeamGridPage));
        }

        private void BTTNRefPlay(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MenuPageRP));
        }
    }
}
