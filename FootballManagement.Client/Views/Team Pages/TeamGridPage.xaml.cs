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

namespace FootballManagement.Client.Views.Team_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class TeamGridPage : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        List<Team> teams = new List<Team>();

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


        public TeamGridPage()
        {
            onLoad();
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        async public void onLoad()
        {
            teams = await _footballService.GetListTeamAsync();
            foreach (var t in teams)
            {
                Button b = new Button();
                b.Background = new SolidColorBrush(Color.FromArgb(242, 242, 242, 242));
                b.Foreground = new SolidColorBrush(Colors.Black);
                b.Opacity = 60;
                b.Width = 170;
                b.Height = 170;
                b.Content = t.Name;
                GridTeams.Items.Add(b);
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

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ClickBTTNEdit(object sender, RoutedEventArgs e)
        {
            if (GridTeams.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridTeams.SelectedItem;
                Team t = teams.FirstOrDefault(x => x.Name == (string)button.Content);
                this.Frame.Navigate(typeof(EditTeamPage), t);
            }
            else
                Notifications.Text = "No hay torneo por editar";
        }

        async private void ClickBTTNDelete(object sender, RoutedEventArgs e)
        {
            if (GridTeams.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridTeams.SelectedItem;
                Team t = teams.FirstOrDefault(x => x.Name == (string)button.Content);
                List<Match> matches = await _footballService.GetListMatchAsync();
                matches = matches.Where(x => x.Team.Id== t.Id || x.Team1.Id == t.Id).ToList();
                if (matches.Count() == 0)
                {
                    bool response = await _footballService.DeleteTeamAsync(t);
                    if (response == true)
                    {
                        this.Frame.Navigate(typeof(TeamGridPage));
                    }
                }
                else
                {
                    Notifications.Text = "No se puede eliminar equipo, hay partidos dependientes a el";
                }
            }
            else
                Notifications.Text = "No hay equipo por eliminar";
        }

        private void TeamSelection(object sender, SelectionChangedEventArgs e)
        {
            GridTeams.SelectionMode = ListViewSelectionMode.Single;
            if (GridTeams.SelectedItem != null)
            {
                AppBar.IsOpen = true;
            }
            else if (GridTeams.SelectedItem == null)
            {
                AppBar.IsOpen = false;
            }
        }

        private void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            GridTeams.SelectedIndex = -1;
            AppBar.IsOpen = false;
        }

        private void ClickBTTNHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ClickBTTNAdd(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTeamPage));
        }
    }
}
