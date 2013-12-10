using FootballManagement.Client.FootballManagementServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace FootballManagement.Client.Views.Match_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MatchGridPage : FootballManagement.Client.Common.LayoutAwarePage
    {
        Tournament tournament = new Tournament();
        List<Match> matches = new List<Match>();
        List<Team> teams = new List<Team>();
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();

        public MatchGridPage()
        {
            this.InitializeComponent();
            AddMatches();
        }

        async public void AddMatches()
        {
            matches = await _footballService.GetListMatchAsync();
            matches = matches.Where(x => x.Tournament.Id == tournament.Id).ToList();
            GridTournaments.ItemsSource = matches;

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            tournament = e.Parameter as Tournament;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void ClickBTTNHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        async private void ClickBTTNAdd(object sender, RoutedEventArgs e)
        {
            teams = await _footballService.GetListTeamAsync();
            teams = teams.Where(x=> x.Tournament == tournament).ToList();
            if(teams.Count!= 0)
                this.Frame.Navigate(typeof(AddMatchPage), tournament);
            else
                Notifications.Text ="No hay equipos disponibles para este torneo";
        }

        async private void ClickBTTNDelete(object sender, RoutedEventArgs e)
        {
            if (GridTournaments.SelectedItem != null)
            {
                Notifications.Text = "";
                Match m = matches.FirstOrDefault(x => x == GridTournaments.SelectedItem);
                bool response = await _footballService.DeleteMatchAsync(m);
                if (response == true)
                    this.Frame.Navigate(typeof(MatchGridPage), tournament);
                else
                    Notifications.Text = "Partido no se pudo eliminar";
            }
            else
                Notifications.Text = "No hay partido por eliminar";
        }

        private void ClickBTTNEdit(object sender, RoutedEventArgs e)
        {
            if (GridTournaments.SelectedItem != null)
            {
                Notifications.Text = "";
                Match m = matches.FirstOrDefault(x => x == GridTournaments.SelectedItem);
                this.Frame.Navigate(typeof(EditMatchPage), m);
            }
            else
                Notifications.Text = "No hay partido por editar";
        }

        private void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            GridTournaments.SelectedIndex = -1;
            AppBar.IsOpen = false;
        }

        private void MatchSelection(object sender, SelectionChangedEventArgs e)
        {
            GridTournaments.SelectionMode = ListViewSelectionMode.Single;

            if (GridTournaments.SelectedItem != null)
            {
                AppBar.IsOpen = true;
            }
            else if (GridTournaments.SelectedItem == null)
            {
                AppBar.IsOpen = false;
            }
        }

    }
}
