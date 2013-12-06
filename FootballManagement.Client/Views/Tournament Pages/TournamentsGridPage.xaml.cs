using FootballManagement.Client.FootballManagementServiceReference;
using FootballManagement.Client.Views.Match_Pages;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace FootballManagement.Client.Views.Tournament_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class TournamentsGridPage : FootballManagement.Client.Common.LayoutAwarePage
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        List<Button> buttons = new List<Button>();
        List<Tournament> tournaments;

        public TournamentsGridPage()
        {
            this.InitializeComponent();
            AddGrid();

        }

        async void AddGrid()
        {
            tournaments = await _footballService.GetListTournamentAsync();
            foreach (var t in tournaments)
            {
                Button b = new Button();
                b.Background = new SolidColorBrush(Color.FromArgb(242, 242, 242, 242));
                b.Foreground = new SolidColorBrush(Colors.Black);
                b.Opacity = 60;
                b.Width = 170;
                b.Height = 170;
                b.Content = t.Name;
                buttons.Add(b);
                GridTournaments.Items.Add(b);
            }
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

        //Calls this event
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //List<Tournament> tournaments = await _footballService.GetListTournamentAsync();
            //var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            //foreach (var t in tournaments)
            //{
            //    stackPanel.Children.Add(new Button { Content = t.Name });
            //}
        }

        private void Page_Loaded(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            GridTournaments.SelectedIndex = -1;
            AppBar.IsOpen = false;
        }

        private void TournamentSelection(object sender, SelectionChangedEventArgs e)
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

        private void ClickBTTNEdit(object sender, RoutedEventArgs e)
        {
            if (GridTournaments.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridTournaments.SelectedItem;
                Tournament t = tournaments.FirstOrDefault(x => x.Name == (string)button.Content);
                this.Frame.Navigate(typeof(EditTournamentPage), t);
            }
            else
                Notifications.Text = "No hay torneo por editar";

        }

        async private void ClickBTTNDelete(object sender, RoutedEventArgs e)
        {
            if (GridTournaments.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridTournaments.SelectedItem;
                Tournament t = tournaments.FirstOrDefault(x => x.Name == (string)button.Content);
                bool response = await _footballService.DeleteTournamentAsync(t);
                if (response == true)
                {
                    this.Frame.Navigate(typeof(TournamentsGridPage));
                }
            }
            else
                Notifications.Text = "No hay torneo por eliminar";

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void ClickBTTNHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ClickBTTNMatches(object sender, RoutedEventArgs e)
        {
            if (GridTournaments.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridTournaments.SelectedItem;
                Tournament t = tournaments.FirstOrDefault(x => x.Name == (string)button.Content);
                this.Frame.Navigate(typeof(MatchGridPage),t);
            }
            else
                Notifications.Text = "No hay torneo seleccionado";
        }

        private void SearchBoxEventsQuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            this.Frame.Navigate(typeof(SearchBox));
        }

    }
}
