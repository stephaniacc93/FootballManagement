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
    public sealed partial class EditPlayerPage : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        Player player = new Player();
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


        public EditPlayerPage()
        {
            this.InitializeComponent();
            onLoad();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
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

        async public void onLoad()
        {
            CBteam.ItemsSource = await _footballService.GetListTeamAsync();
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


        private void Home(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        async private void EditPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (TXTplayerName.Text.Length >= 1)
            {
                Player p = new Player();
                p.Name = TXTplayerName.Text;
                p.Id = player.Id;
                ComboBoxItem cbItem = (ComboBoxItem)CBgender.SelectedItem;
                p.Gender = cbItem.Content.ToString();
                p.IsAuthorized = true;
                if (CBcaptain.IsEnabled == true)
                    p.IsCaptain = (bool)CBcaptain.IsChecked;
                else
                    p.IsCaptain = false;

                p.Birthday = DatePickerBirthday.Date.DateTime;
                p.Team = (Team)CBteam.SelectedItem;
                Player response = await _footballService.UpdatePlayerAsync(p);
                if (response.Id != 0)
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
            if (player.IsCaptain == true)
                CBcaptain.IsEnabled = true;
            else
                CBcaptain.IsEnabled = true;
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            player = e.Parameter as Player;

            List<Team> teams = await _footballService.GetListTeamAsync();
            for (int i = 0; i < teams.Count(); i++)
            {
                if (teams.ElementAt(i).Id == player.Team.Id)
                {
                    CBteam.SelectedIndex = i;
                    break;
                }
            }

            TXTplayerName.Text = player.Name;
            DatePickerBirthday.Date = player.Birthday.Date;
            if (player.Gender == "Masculino")
                CBgender.SelectedItem = Men;
            else
                CBgender.SelectedItem = Women;

            List<Player> players = await _footballService.GetListPlayerAsync();
            Team t = player.Team;
            List<Player> capitans = players.Where(x => x.IsCaptain == true).ToList();
            bool response = capitans.Any(x => x.Team.Id == t.Id);

            if (player.IsCaptain == true)
            {
                CBcaptain.IsChecked = true;
                CBcaptain.IsEnabled = true;
            }
            else if (response == true)
            {
                CBcaptain.IsEnabled = false;
                CBcaptain.IsChecked = false;
            }

            else
                CBcaptain.IsEnabled = true;


        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
    }
}
