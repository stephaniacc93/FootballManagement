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
    public sealed partial class CardFlyout : SettingsFlyout
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        List<Player> players = new List<Player>();
        Match match = new Match();

        public CardFlyout(Match m)
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

        async private void addCard_Click(object sender, RoutedEventArgs e)
        {
            if (CBType.SelectedItem != null && CBPlayers.SelectedItem != null)
            {
                Notifications.Text = " ";
                Card card = new Card();
                card.Player = (Player)CBPlayers.SelectedItem;
                card.Date = (match.MatchDate.Date + TPCardTime.Time);
                card.Match = match;
                ComboBoxItem cbItem = (ComboBoxItem)CBType.SelectedItem;
                if (cbItem.Content.ToString() == "Tarjeta Roja")
                {
                    card.isRedCard = true;
                }
                else
                    card.isRedCard = false;

                bool response = await _footballService.CreateCardAsync(card);
                if (response == true)
                {
                    this.Hide();
                }
                else
                    Notifications.Text = "La tarjeta no se ha registrado";
            }
            else
            {
                Notifications.Text = "Revisa los datos por ingresar";
            }
        }
    }
}
