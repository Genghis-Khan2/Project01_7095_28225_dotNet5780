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
using BE;

namespace PLWPF.Host_Windows
{
    /// <summary>
    /// Interaction logic for HostingUnitInfo.xaml
    /// </summary>
    public partial class HostingUnitInfo : Window
    {
        public HostingUnitInfo(HostingUnit hu)
        {
            InitializeComponent();
            KeyLab.Content = hu.HostingUnitKey.ToString();

        }
    }
}
