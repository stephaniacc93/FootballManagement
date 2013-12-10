using FootballManagement.Client.Common;
using FootballManagement.Client.FootballManagementServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace FootballManagement.Client.Views.Referee_and_Player_Pages.Player_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AddPlayerPage : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
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


        public AddPlayerPage()
        {
            this.InitializeComponent();
            onLoad();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        async public void onLoad()
        {
            CBteam.DataContext = await _footballService.GetListTeamAsync();

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

        async private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (TXTplayerName.Text.Length >= 1)
            {
                List<Player> players = await _footballService.GetListPlayerAsync();
                if (players.Exists(x => x.Name == TXTplayerName.Text) != true)
                {
                    Player newPlayer = new Player();
                    ComboBoxItem cbItem = (ComboBoxItem)CBgender.SelectedItem;
                    newPlayer.Name = TXTplayerName.Text;
                    newPlayer.Gender = cbItem.Content.ToString();
                    newPlayer.IsAuthorized = true;
                    if (CBcaptain.IsEnabled == true)
                        newPlayer.IsCaptain = (bool)CBcaptain.IsChecked;
                    else
                        newPlayer.IsCaptain = false;

                    newPlayer.Birthday = DatePickerBirthday.Date.DateTime;
                    newPlayer.Team = (Team)CBteam.SelectedItem;
                    bool response = await _footballService.CreatePlayerAsync(newPlayer);
                    if (response == true)
                    {
                        this.Frame.Navigate(typeof(PlayerGridPage));
                    }
                    else
                    {
                        LBLnotifications.Text = "Su jugador no ha sido registrado";
                    }
                }
                else
                {
                    LBLnotifications.Text = "El nombre de este jugador ya es existente";
                }
            }
            else
            {
                LBLnotifications.Text = "Revise la informacion que ha ingresado";
            }
        }

        async private void CBteam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Player> players = await _footballService.GetListPlayerAsync();
            Team t = (Team)CBteam.SelectedItem;
            List<Player> capitans = players.Where(x => x.IsCaptain == true).ToList();
            bool response = capitans.Any(x => x.Team.Id == t.Id);
            if (response == true)
                CBcaptain.IsEnabled = false;
            else
                CBcaptain.IsEnabled = true;
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
