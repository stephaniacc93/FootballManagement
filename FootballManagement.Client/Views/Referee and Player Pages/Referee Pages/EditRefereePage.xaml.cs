﻿using FootballManagement.Client.Common;
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

namespace FootballManagement.Client.Views.Referee_and_Player_Pages.Referee_Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class EditRefereePage : Page
    {
        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        Referee referee = new Referee();
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


        public EditRefereePage()
        {
            this.InitializeComponent();
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
            referee = e.Parameter as Referee;
            TXTrefereeName.Text = referee.Name;
            TXTdegree.Text = referee.Degree;
            DatePickerBirthday.Date = referee.Birthday.Date;
            if (referee.Gender == "Masculino")
                CBgender.SelectedItem = Men;
            else
                CBgender.SelectedItem = Women;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        async private void BTTNeditReferee_Click(object sender, RoutedEventArgs e)
        {
            if (TXTrefereeName.Text.Length >= 1 && CBgender.SelectedItem != null && TXTdegree.Text.Length >= 1)
            {
                List<Referee> referees = await _footballService.GetListRefereeAsync();
                if (referees.Exists(x => x.Name == TXTrefereeName.Text) != true)
                {
                    Referee newReferee = new Referee();
                    ComboBoxItem cbItem = (ComboBoxItem)CBgender.SelectedItem;
                    newReferee.Name = TXTrefereeName.Text;
                    newReferee.Id = referee.Id;
                    newReferee.Gender = cbItem.Content.ToString();
                    newReferee.Degree = TXTdegree.Text;
                    newReferee.Birthday = DatePickerBirthday.Date.DateTime;
                    Referee response = await _footballService.UpdateRefereeAsync(newReferee);
                    if (response.Id != 0)
                    {
                        this.Frame.Navigate(typeof(RefereeGridPage));
                    }
                    else
                    {
                        LBLnotifications.Text = "Su arbitro no ha sido registrado";
                    }
                }
                else
                {
                    LBLnotifications.Text = "El nombre de este arbitro ya es existente";
                }
            }
            else
            {
                LBLnotifications.Text = "Revise la informacion que ha ingresado";
            }
        }
    }
}
