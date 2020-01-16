﻿using System;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostMenu.xaml
    /// </summary>
    public partial class HostMenu : Window
    {
        BE.Host host;
        public HostMenu(BE.Host host)
        {
            InitializeComponent();
            this.host = host;
            try
            {
                var li = from i in CreateAccount.myBL.getHostingUnitByHost()
                         where i.Key.HostKey == FR_Imp.GetFR().GetHostKey(LoginPage.Username)
                         select new { Hostingunits = i };

                foreach (var i in li)
                {
                    foreach (var item in i.Hostingunits)
                    {
                        GuestRequestStack.Children.Add(new HostingUnitUC(item.HostingUnitName, item.HostingUnitKey, item.Commission));
                    }
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var win = new CreateHostingUnit(host);
            win.Show();
            win.Closed += (s, args) => Show();
            Refresh_Click(sender, e);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var li = from i in CreateAccount.myBL.getHostingUnitByHost()
                         where i.Key.HostKey == FR_Imp.GetFR().GetHostKey(LoginPage.Username)
                         select new { Hostingunits = i };

                foreach (var i in li)
                {
                    foreach (var item in i.Hostingunits)
                    {
                        GuestRequestStack.Children.Add(new HostingUnitUC(item.HostingUnitName, item.HostingUnitKey, item.Commission));
                    }
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }
    }
}
