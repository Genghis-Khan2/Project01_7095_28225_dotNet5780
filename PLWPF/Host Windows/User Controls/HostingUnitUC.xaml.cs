using PLWPF.Host_Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnitUC.xaml
    /// </summary>
    public partial class HostingUnitUC : UserControl
    {
        private HostMenu caller;
        private HostingUnit hu;
        public HostingUnitUC(HostMenu caller, BE.HostingUnit hu)
        {
            InitializeComponent();
            this.hu = hu;
            this.caller = caller;
            HUName.Content = hu.HostingUnitName;
            Key.Content = hu.HostingUnitKey.ToString();
            Commission.Content = hu.Commission.ToString();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            caller.RemoveHostingUnit(hu.HostingUnitKey);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var infoWin = new HostingUnitInfo(hu);
            infoWin.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var updatewin = new HostingUnitInfo(hu);
            updatewin.Show();
            updatewin.ActivateUpdate();
        }

        private void MatchesButton_Click(object sender, RoutedEventArgs e)
        {
            caller.LoadMatchesForHU(hu);
        }
    }
}
