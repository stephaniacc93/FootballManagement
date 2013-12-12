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
    public sealed partial class RefereeFlyout : SettingsFlyout
    {

        FootballManagementServiceClient _footballService = new FootballManagementServiceClient();
        List<Referee> referees = new List<Referee>();
        List<Referee> finalReferees = new List<Referee>();
        List<Referee> lastReferees = new List<Referee>();
        Match match = new Match();

        public RefereeFlyout(Match m, List<Referee> lr)
        {
            this.InitializeComponent();
            match = m;
            lastReferees = lr;
            onLoad();
        }

        async public void onLoad()
        {
            List<Referee> allReferees = new List<Referee>();
            referees = await _footballService.GetListRefereeAsync();

            if (lastReferees.Count() != 0)
            {
                foreach (var last in lastReferees)
                {
                    Referee r = referees.FirstOrDefault(x => x.Id == last.Id);
                    referees.Remove(r);
                }
                allReferees = referees;
            }
            else
                allReferees = referees;
            LBReferees.ItemsSource = allReferees;
        }

        public void AddReferees(object sender, RoutedEventArgs e)
        {
            if (LBReferees.SelectedItems.Count != 0)
            {
                foreach (var s in LBReferees.SelectedItems)
                {
                    finalReferees.Add((Referee)s);
                }
            }
            foreach (var lr in lastReferees)
            {
                finalReferees.Add(lr);
            }
            this.Hide();

        }

        public List<Referee> getList()
        {
            return finalReferees;
        }


    }
}
