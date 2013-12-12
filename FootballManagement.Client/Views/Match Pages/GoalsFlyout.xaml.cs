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

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace FootballManagement.Client.Views.Match_Pages
{
    public sealed partial class GoalsFlyout : SettingsFlyout
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        Match match = new Match();
        List<Player> players = new List<Player>();

        public GoalsFlyout(Match m)
        {
            this.InitializeComponent();
            match = m;
            onLoad();
        }

        public void onLoad()
        {
            foreach (var p in match.Team.Players)
            {
                players.Add(p);
            }
            foreach (var p in match.Team1.Players)
            {
                players.Add(p);
            }
            CBPlayers.ItemsSource = players;
        }

        async private void addGoal_Click(object sender, RoutedEventArgs e)
        {
            if (CBPlayers.SelectedItem != null)
            {
                Notifications.Text = " ";
                Goal goal = new Goal();
                goal.Match = match;
                goal.Player = (Player)CBPlayers.SelectedItem;
                goal.Time = (match.MatchDate.Date + TPGoalTime.Time);
                bool response = await _footballService.CreateGoalAsync(goal);
                if (response == true)
                {
                    this.Hide();
                }
                else
                    Notifications.Text = "El gol no se ha registrado";
            }
            else
            {
                Notifications.Text = "Revisa los datos por ingresar";
            }
        }
    }
}
