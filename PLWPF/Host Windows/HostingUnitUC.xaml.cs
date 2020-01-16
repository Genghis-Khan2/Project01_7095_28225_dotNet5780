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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnitUC.xaml
    /// </summary>
    public partial class HostingUnitUC : UserControl
    {
        public HostingUnitUC(string name, int key, float commission)
        {
            InitializeComponent();
            Name.Content = name;
            Key.Content = key.ToString();
            Commission.Content = commission.ToString();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
