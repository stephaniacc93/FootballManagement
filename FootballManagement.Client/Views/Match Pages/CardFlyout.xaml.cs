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
                if (p.IsAuthorized == true)
                    players.Add(p);
            }
            foreach (var p in match.Team1.Players)
            {
                if (p.IsAuthorized == true)
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
                    card.Player.IsAuthorized = false;
                    Player p = await _footballService.UpdatePlayerAsync(card.Player);
                }
                else
                {
                    card.isRedCard = false;
                    List<Card> yellowCards = await _footballService.GetListCardAsync();
                    yellowCards = yellowCards.Where(x => x.Player.Id == card.Player.Id).ToList(); ;
                    foreach (var c in yellowCards)
                    {
                        if (c.Match.Id == card.Match.Id)
                        {
                            card.Player.IsAuthorized = false;
                            Player p = await _footballService.UpdatePlayerAsync(card.Player);
                            break;
                        }
                    }
                }

                bool response = await _footballService.CreateCardAsync(card);
                List<Player> AllPlayers = await _footballService.GetListPlayerAsync();
                if (response == true)
                {
                    foreach (var p in AllPlayers.Where(x => x.IsAuthorized == false))
                    {
                        if (p.Id != card.Player.Id)
                        {
                            p.IsAuthorized = true;
                            Player player = await _footballService.UpdatePlayerAsync(p);
                        }
                    }
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
