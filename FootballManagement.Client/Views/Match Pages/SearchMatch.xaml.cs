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

namespace FootballManagement.Client.Views.Match_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SearchMatch : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        List<Match> matches = new List<Match>();
        Tournament tournament = new Tournament();
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


        public SearchMatch()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        void onLoad()
        {
            foreach (var m in matches)
            {
                Button b = new Button();
                b.Background = new SolidColorBrush(Color.FromArgb(242, 242, 242, 242));
                b.Foreground = new SolidColorBrush(Colors.Black);
                b.Opacity = 60;
                b.Width = 170;
                b.Height = 170;
                b.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                StackPanel sp = new StackPanel();
                sp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                TextBlock tb = new TextBlock();
                tb.Text = m.Team.Name;
                TextBlock tb2 = new TextBlock();
                tb2.Text = " VS ";
                TextBlock tb3 = new TextBlock();
                tb.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                tb3.Text = m.Team1.Name;
                tb2.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                tb3.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                sp.Children.Add(tb);
                sp.Children.Add(tb2);
                sp.Children.Add(tb3);
                b.Content = sp;
                b.Tag = m.Id;
                GridTournaments.Items.Add(b);
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


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            matches = e.Parameter as List<Match>;
            foreach (var m in matches)
            {
                tournament = m.Tournament;
                break;
            }
            onLoad();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        async private void ClickBTTNDelete(object sender, RoutedEventArgs e)
        {
            if (GridTournaments.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridTournaments.SelectedItem;
                Match m = matches.FirstOrDefault(x => x.Id == (int)button.Tag);
                List<Card> cards = await _footballService.GetListCardAsync();
                List<Goal> goals = await _footballService.GetListGoalAsync();

                cards = cards.Where(x => x.Match.Id == m.Id).ToList();
                goals = goals.Where(x => x.Match.Id == m.Id).ToList();

                if (goals.Count != 0 || cards.Count() != 0)
                {
                    foreach (var g in goals)
                    {
                        await _footballService.DeleteGoalAsync(g);
                    }
                    foreach (var c in cards)
                    {
                        await _footballService.DeleteCardAsync(c);
                    }
                }

                bool response = await _footballService.DeleteMatchAsync(m);
                if (response == true)
                    this.Frame.Navigate(typeof(MatchGridPage), tournament);
                else
                    Notifications.Text = "Partido no se pudo eliminar";
            }
            else
                Notifications.Text = "No hay partido por eliminar";
        }

        async private void ClickBTTNEdit(object sender, RoutedEventArgs e)
        {
            if (GridTournaments.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridTournaments.SelectedItem;
                Match m = matches.FirstOrDefault(x => x.Id == (int)button.Tag);
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

        private void ClickBTTNHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}
