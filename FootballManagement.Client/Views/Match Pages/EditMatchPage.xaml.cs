using FootballManagement.Client.FootballManagementServiceReference;
using FootballManagement.Client.Common;
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
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace FootballManagement.Client.Views.Match_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class EditMatchPage : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        Match match = new Match();
        List<Referee> referees = new List<Referee>();

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            match = e.Parameter as Match;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        public EditMatchPage()
        {
            this.InitializeComponent();
            load();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        async public void load()
        {
            List<Team> teams = await _footballService.GetListTeamAsync();
            referees = await _footballService.GetListRefereeAsync();
            teams = teams.Where(x => x.Tournament == match.Tournament).ToList();  
            referees = referees.Where(x => match.Referees.Any(y => x.Id == y.Id)).ToList();

            CBMatchTeamHome.DataContext = teams;
            CBMatchTeamVisitor.DataContext = teams;
            GVReferee.DataContext = referees;
            int teamHomeIndex = 0;
            int teamVisitorIndex = 0;
            Team home = await _footballService.ReadTeamAsync(match.Team.Id);
            Team visitor = await _footballService.ReadTeamAsync(match.Team1.Id);

            for (int i = 0; i < teams.Count; i++)
            {
                if (teams.ElementAt(i).Id == home.Id)
                {
                    teamHomeIndex = i;
                }

                if (teams.ElementAt(i).Id == visitor.Id)
                {
                    teamVisitorIndex = i;
                }
            }

            CBMatchTeamHome.SelectedIndex = teamHomeIndex;
            CBMatchTeamVisitor.SelectedIndex = teamVisitorIndex;
            DatePickerMatch.Date = match.MatchDate.Date;
            TimePickerMatch.Time = match.MatchDate.TimeOfDay;
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

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MatchGridPage), match.Tournament);
        }

        async private void BTTNeditMatch_Click(object sender, RoutedEventArgs e)
        {
            match.Team = (Team)CBMatchTeamHome.SelectionBoxItem;
            match.Team1 = (Team)CBMatchTeamVisitor.SelectionBoxItem;
            match.MatchDate = (DatePickerMatch.Date + TimePickerMatch.Time).DateTime;
            match.Referees = referees;

            if (match.Referees.Count != 0 && match.Team != null && match.Team1 != null && match.MatchDate != null)
            {
                try
                {
                    Match m = await _footballService.UpdateMatchAsync(match);
                    if (m.Id != 0)
                        this.Frame.Navigate(typeof(MatchGridPage), match.Tournament);
                    else
                        LBLnotifications.Text = "Favor de revisar los datos";
                }
                catch (Exception v)
                {
                    throw v;
                }
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


            #endregion
        }

        async private void BTTNDeleteReferee(object sender, RoutedEventArgs e)
        {
            List<Referee> refereeNumber = (List<Referee>)GVReferee.Tag;
            Referee x = refereeNumber.First();
            Referee r = await _footballService.ReadRefereeAsync(x.Id);
            foreach (var a in referees)
            {
                if(a.Id == r.Id)
                {
                    referees.Remove(a);
                    if (referees.Count == 0)
                        break;
                }
            }
            GVReferee.ItemsSource = null;
            GVReferee.ItemsSource = referees;
            StackPanel s = new StackPanel();
            TextBlock t = new TextBlock() { TextWrapping = TextWrapping.Wrap };
            //fill in some lebrowski ipsum
            t.Text = "This Chinaman who peed on my rug, I can't go give him a bill so what the fuck are you talking about? Please see him, Jeffrey. He's a good man. And thorough. Well sure, look at it! Young trophy wife, I mean, in the parlance of our times, owes money all over town, including to known pornographers— and that's cool, that's cool. DO YOU SEE WHAT HAPPENS, LARRY?\r\rI don't like you sucking around bothering our citizens, Lebowski. Yeah? What do you think happens when you get divorced? You turn in your library card? Get a new driver's license? Stop being Jewish? Do you have any kalhua? If the plan gets too complex something always goes wrong. If there's one thing I learned in Nam—. \"My son can't hold a job, my daughter's married to a fuckin' loser, and I got a rash on my ass so bad I can't hardly siddown. But you know me. I can't complain.\".";
            t.Margin = new Thickness(4);

            s.Children.Add(new ToggleSwitch() { Header = "useless switch" });
            s.Children.Add(t);

            //now create the flyout
            Flyout f = new Flyout();

        }
    }
}