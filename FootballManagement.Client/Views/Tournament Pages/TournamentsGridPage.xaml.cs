using FootballManagement.Client.FootballManagementServiceReference;
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

namespace FootballManagement.Client.Views.Tournament_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class TournamentsGridPage : FootballManagement.Client.Common.LayoutAwarePage
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        List<Button> buttons = new List<Button>();
        public TournamentsGridPage()
        {
            this.InitializeComponent();
            AddGrid();

        }

        async void AddGrid()
        {
            List<Tournament> tournaments = await _footballService.GetListTournamentAsync();
            foreach (var t in tournaments)
            {
                Button b = new Button();
                b.Background = new SolidColorBrush(Colors.DarkRed);
                b.Content = t.Name;
                b.Width = 170;
                b.Height = 170;
                buttons.Add(b);
                GridViewTournaments.Items.Add(b);
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

        //Calls this event
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //List<Tournament> tournaments = await _footballService.GetListTournamentAsync();
            //var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            //foreach (var t in tournaments)
            //{
            //    stackPanel.Children.Add(new Button { Content = t.Name });
            //}
        }

        private void Page_Loaded(object sender, DependencyPropertyChangedEventArgs e)
        {

        }


    }
}
