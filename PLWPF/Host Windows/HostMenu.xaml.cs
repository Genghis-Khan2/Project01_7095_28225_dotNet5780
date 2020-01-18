using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using FR;
using PLWPF.Host_Windows;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostMenu.xaml
    /// </summary>
    public partial class HostMenu : Window
    {
        Host host;
        public HostMenu(Host host)
        {
            InitializeComponent();
            this.host = host;
            Refresh();
        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            var win = new CreateHostingUnit(host);
            win.Show();
        }

        private void Refresh_On_Close(object sender, EventArgs e)
        {
            Show();
            Refresh();
        }

        private void Refresh()
        {
            HostingUnitStack.Children.Clear();
            HostingUnitStack.Children.Add(Resources["TitlesBar"] as Grid);
            try
            {
                var li = from i in CreateAccount.myBL.getHostingUnitByHost()
                         where i.Key.HostKey == FR_Imp.GetFR().GetHostKey(LoginPage.Username)
                         select new { Hostingunits = i };

                foreach (var i in li)
                {
                    foreach (var item in i.Hostingunits)
                    {
                        HostingUnitStack.Children.Add(new HostingUnitUC(this, item));
                    }
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        internal void RemoveHostingUnit(int key)
        {
            System.Media.SystemSounds.Hand.Play();
            var dialogResult = MessageBox.Show("Are you sure you want to delete the hosting unit?\nNote! This will permanently delete the hosting unit and all related Orders!", "Alert!", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                CreateAccount.myBL.RemoveHostingUnit(key);
                Refresh();
            }
        }
    }
}
