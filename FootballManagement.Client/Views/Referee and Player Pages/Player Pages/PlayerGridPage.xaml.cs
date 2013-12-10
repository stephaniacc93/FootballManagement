using FootballManagement.Client.Common;
using FootballManagement.Client.FootballManagementServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace FootballManagement.Client.Views.Referee_and_Player_Pages.Player_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PlayerGridPage : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        List<Player> players = new List<Player>();
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public PlayerGridPage()
        {
            this.InitializeComponent();
            onLoad();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        async public void onLoad()
        {
            players = await _footballService.GetListPlayerAsync();
            foreach (var p in players)
            {
                Button b = new Button();
                b.Background = new SolidColorBrush(Color.FromArgb(242, 242, 242, 242));
                b.Foreground = new SolidColorBrush(Colors.Black);
                b.Opacity = 60;
                b.Width = 170;
                b.Height = 170;
                b.Content = p.Name;
                GridPlayers.Items.Add(b);
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private void ClickBTTNHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void RefereeSelection(object sender, SelectionChangedEventArgs e)
        {
            GridPlayers.SelectionMode = ListViewSelectionMode.Single;
            if (GridPlayers.SelectedItem != null)
            {
                AppBar.IsOpen = true;
            }
            else if (GridPlayers.SelectedItem == null)
            {
                AppBar.IsOpen = false;
            }
        }

        private void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            GridPlayers.SelectedIndex = -1;
            AppBar.IsOpen = false;
        }

        async private void ClickBTTNDelete(object sender, RoutedEventArgs e)
        {
            if (GridPlayers.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridPlayers.SelectedItem;
                Player p = players.FirstOrDefault(x => x.Name == (string)button.Content);
                if (p.Team == null)
                {
                    bool response = await _footballService.DeletePlayerAsync(p);
                    if (response == true)
                    {
                        this.Frame.Navigate(typeof(PlayerGridPage));
                    }
                }
                else
                {
                    Notifications.Text = "No se puede eliminar jugador, hay torneos dependientes a el";
                }
            }
            else
                Notifications.Text = "No hay jugador por eliminar";
        }

        private void ClickBTTNEdit(object sender, RoutedEventArgs e)
        {
            if (GridPlayers.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridPlayers.SelectedItem;
                Player p = players.FirstOrDefault(x => x.Name == (string)button.Content);
                this.Frame.Navigate(typeof(EditPlayerPage), p);
            }
            else
                Notifications.Text = "No hay jugador por editar";
        }

        async private void BTTNAddPlayer(object sender, RoutedEventArgs e)
        {
            List<Team> teams = await _footballService.GetListTeamAsync();
            if (teams.Count() != 0)
                this.Frame.Navigate(typeof(AddPlayerPage));
            else
                Notifications.Text = "No hay equipos disponibles";
        }
    }
}
