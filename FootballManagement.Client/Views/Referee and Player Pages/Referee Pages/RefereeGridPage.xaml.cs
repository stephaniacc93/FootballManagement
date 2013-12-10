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

namespace FootballManagement.Client.Views.Referee_and_Player_Pages.Referee_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class RefereeGridPage : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
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


        public RefereeGridPage()
        {
            this.InitializeComponent();
            onLoad();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        async public void onLoad()
        {
            referees = await _footballService.GetListRefereeAsync();
            foreach (var r in referees)
            {
                Button b = new Button();
                b.Background = new SolidColorBrush(Color.FromArgb(242, 242, 242, 242));
                b.Foreground = new SolidColorBrush(Colors.Black);
                b.Opacity = 60;
                b.Width = 170;
                b.Height = 170;
                b.Content = r.Name;
                b.Tag = r.Id;
                GridReferees.Items.Add(b);
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

        private void ClickBTTNHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        async private void ClickBTTNDelete(object sender, RoutedEventArgs e)
        {
            if (GridReferees.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridReferees.SelectedItem;
                Referee r = referees.FirstOrDefault(x => x.Id == (int)button.Tag);
                List<Referee> inUse = referees;
                referees = referees.Where(x => x.Tournaments.Count != 0).ToList();
                if (referees.Count() == 0)
                {
                    bool response = await _footballService.DeleteRefereeAsync(r);
                    if (response == true)
                    {
                        this.Frame.Navigate(typeof(RefereeGridPage));
                    }
                }
                else
                {
                    Notifications.Text = "No se puede eliminar arbitro, hay torneos dependientes a el";
                }
            }
            else
                Notifications.Text = "No hay arbitro por eliminar";
        }

        private void ClickBTTNEdit(object sender, RoutedEventArgs e)
        {
            if (GridReferees.SelectedItem != null)
            {
                Notifications.Text = "";
                Button button = (Button)GridReferees.SelectedItem;
                Referee r = referees.FirstOrDefault(x => x.Id == (int)button.Tag);
                this.Frame.Navigate(typeof(EditRefereePage), r);
            }
            else
                Notifications.Text = "No hay arbitro por editar";
        }

        private void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            GridReferees.SelectedIndex = -1;
            AppBar.IsOpen = false;
        }

        private void RefereeSelection(object sender, SelectionChangedEventArgs e)
        {
            GridReferees.SelectionMode = ListViewSelectionMode.Single;
            if (GridReferees.SelectedItem != null)
            {
                AppBar.IsOpen = true;
            }
            else if (GridReferees.SelectedItem == null)
            {
                AppBar.IsOpen = false;
            }
        }

        private void BTTNAddReferee(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddRefereePage));
        }
    }
}
