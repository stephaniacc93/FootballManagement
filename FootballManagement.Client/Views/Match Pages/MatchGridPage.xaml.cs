﻿using FootballManagement.Client.FootballManagementServiceReference;
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
            teams = teams.Where(x => x.Tournament.Id == tournament.Id).ToList();
            if (teams.Count != 0)
                this.Frame.Navigate(typeof(AddMatchPage), tournament);
            else
                Notifications.Text = "No hay equipos disponibles para este torneo";
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

                foreach (var p in m.Team.Players)
                {
                    p.IsAuthorized = true;
                    await _footballService.UpdatePlayerAsync(p);
                }

                foreach (var p in m.Team1.Players)
                {
                    p.IsAuthorized = true;
                    await _footballService.UpdatePlayerAsync(p);
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

        private void ClickBTTNEdit(object sender, RoutedEventArgs e)
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

        private void doSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Search.Text != "")
            {
                string text = Search.Text;
                List<Match> matchesSearch1 = matches.Where(x => x.Team.Name.ToLower().StartsWith(text)).ToList();
                List<Match> matchesSearch2 = matches.Where(x => x.Team1.Name.ToLower().StartsWith(text)).ToList();

                foreach (var m in matchesSearch2)
                {
                    if (matchesSearch1.Any(x => x.Id == m.Id) == false)
                        matchesSearch1.Add(m);
                }
                this.Frame.Navigate(typeof(SearchMatch), matchesSearch1);
            }
        }

    }
}
