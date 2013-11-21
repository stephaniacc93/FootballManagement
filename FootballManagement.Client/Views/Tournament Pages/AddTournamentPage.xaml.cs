using FootballManagement.Client.FootballManagementServiceReference;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace FootballManagement.Client.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AddTournamentPage : FootballManagement.Client.Common.LayoutAwarePage
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();

        public AddTournamentPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        async private void BTTNaddTournament_Click(object sender, RoutedEventArgs e)
        {
            if(TXTtournamentName.Text.Length >= 1)
            {
                Tournament newTournament = new Tournament();
                newTournament.Name = TXTtournamentName.Text;
                bool response = await _footballService.CreateTournamentAsync(newTournament);
                if (response == true)
                {
                    this.Frame.Navigate(typeof(TournamentMenuPage));
                }
                else
                {
                    LBLnotifications.Text = "Su torneo no ha sido registrado";
                }
            }
            else
            {
                LBLnotifications.Text = "Revise la informacion que ha ingresado";
            }
        }
    }
}
