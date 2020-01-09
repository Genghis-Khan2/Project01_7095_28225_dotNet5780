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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddGuestRequest.xaml
    /// </summary>
    public partial class AddGuestRequest : Window
    {
        public AddGuestRequest()
        {
            InitializeComponent();
            var AreaEnums = Enum.GetValues(typeof(Enums.Area));
            foreach (var item in AreaEnums)
            {
                AreaComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            AreaComboBox.SelectedIndex = 0;

            var isInterested = Enum.GetValues(typeof(Enums.IsInterested));
            foreach (var item in isInterested)
            {
                PoolComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                JaccuziComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                GardenComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                ChildrenAttractionsComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            PoolComboBox.SelectedIndex = 1;
            JaccuziComboBox.SelectedIndex = 1;
            GardenComboBox.SelectedIndex = 1;
            ChildrenAttractionsComboBox.SelectedIndex = 1;

            var hostingUnitType = Enum.GetValues(typeof(Enums.HostingUnitType));
            foreach (var item in hostingUnitType)
            {
                HostingUnitComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            HostingUnitComboBox.SelectedIndex = 0;
        }

        private void GardenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
