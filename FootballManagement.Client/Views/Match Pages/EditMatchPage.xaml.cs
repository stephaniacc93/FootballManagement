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
        RefereeFlyout rf;
        GoalsFlyout gf;
        List<Goal> goals = new List<Goal>();
        List<Card> cards = new List<Card>();
        List<Team> teams = new List<Team>();
        List<Goal> goalsAdded = new List<Goal>();
        List<Card> cardsAdded = new List<Card>();

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
            teams = await _footballService.GetListTeamAsync();
            referees = await _footballService.GetListRefereeAsync();
            referees = referees.Where(x => match.Referees.Any(y => x.Id == y.Id)).ToList();

            TeamHome.Text = match.Team.Name;
            TeamVisitor.Text = match.Team1.Name;
            GVReferee.DataContext = referees;

            goals = await _footballService.GetListGoalAsync();
            goals = goals.Where(x => x.Match.Id == match.Id).ToList();
            GVGoles.ItemsSource = null;
            GVGoles.ItemsSource = goals;

            cards = await _footballService.GetListCardAsync();
            cards = cards.Where(x => x.Match.Id == match.Id).ToList();
            GVTarjetas.ItemsSource = null;
            GVTarjetas.ItemsSource = cards;

            Team home = await _footballService.ReadTeamAsync(match.Team.Id);
            Team visitor = await _footballService.ReadTeamAsync(match.Team1.Id);

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

        async private void GoBack(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MatchGridPage), match.Tournament);
        }

        async private void BTTNeditMatch_Click(object sender, RoutedEventArgs e)
        {
            match.Team = match.Team;
            match.Team1 = match.Team1;
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

            else
            {
                LBLnotifications.Text = "Revisa tus datos, estan incompletos";
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


        private void AddReferee_Click(object sender, RoutedEventArgs e)
        {
            rf = new RefereeFlyout(match, referees);
            rf.Show();
            rf.LostFocus += rf_LostFocus;
        }

        void rf_LostFocus(object sender, RoutedEventArgs e)
        {
            if(rf.getList().Count() != 0)
                referees = rf.getList();
            GVReferee.ItemsSource = null;
            GVReferee.ItemsSource = referees;
            match.Referees = referees;
        }

        private void AddGoal_Click(object sender, RoutedEventArgs e)
        {
            gf = new GoalsFlyout(match);
            gf.Show();
            gf.LostFocus += gf_LostFocus;
        }

        async void gf_LostFocus(object sender, RoutedEventArgs e)
        {
            goals = await _footballService.GetListGoalAsync();
            goals = goals.Where(x => x.Match.Id == match.Id).ToList();
            GVGoles.ItemsSource = null;
            GVGoles.ItemsSource = goals;
            foreach (var g in gf.getList())
            {
                goalsAdded.Add(g);
            }

        }

        async private void userTapped(object sender, TappedRoutedEventArgs e)
        {
            Referee x = (Referee)GVReferee.SelectedItem;
            Referee refereeToBeDeleted = await _footballService.ReadRefereeAsync(x.Id);
            foreach (var referee in referees)
            {
                if (referee.Id == refereeToBeDeleted.Id)
                {
                    referees.Remove(referee);
                    break;
                }
            }
            GVReferee.ItemsSource = null;
            GVReferee.ItemsSource = referees;

        }

        async private void userTappedGoal(object sender, TappedRoutedEventArgs e)
        {
            Goal g = (Goal)GVGoles.SelectedItem;
            Goal goalToBeDeleted = await _footballService.ReadGoalAsync(g.Id);
            bool response = await _footballService.DeleteGoalAsync(goalToBeDeleted);
            if(response == true)
            {
                goals = await _footballService.GetListGoalAsync();
                goals = goals.Where(x => x.Match.Id == match.Id).ToList();
                GVGoles.ItemsSource = null;
                GVGoles.ItemsSource = goals;
            }
            
        }

        async private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            match = await _footballService.ReadMatchAsync(match.Id);
            CardFlyout cf = new CardFlyout(match);
            cf.Show();
            cf.LostFocus += cf_LostFocus;
        }

        async void cf_LostFocus(object sender, RoutedEventArgs e)
        {
            cards = await _footballService.GetListCardAsync();
            cards = cards.Where(x => x.Match.Id == match.Id).ToList();
            GVTarjetas.ItemsSource = null;
            GVTarjetas.ItemsSource = cards;
        }

        async private void userTappedCard(object sender, TappedRoutedEventArgs e)
        {
            Card c = (Card)GVTarjetas.SelectedItem;
            Card cardToBeDeleted = await _footballService.ReadCardAsync(c.Id);

            cardToBeDeleted.Player.IsAuthorized = true;
            await _footballService.UpdatePlayerAsync(cardToBeDeleted.Player);

            bool response = await _footballService.DeleteCardAsync(cardToBeDeleted);
            if (response == true)
            {
                cards = await _footballService.GetListCardAsync();
                cards = cards.Where(x=> x.Match.Id == match.Id).ToList();
                GVTarjetas.ItemsSource = null;
                GVTarjetas.ItemsSource = cards;
            }
        }

        private void DatePickerMatch_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            match.MatchDate = (DatePickerMatch.Date + match.MatchDate.TimeOfDay).DateTime;
        }

        private void TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            match.MatchDate = (match.MatchDate.Date + TimePickerMatch.Time);
        }

    }
}