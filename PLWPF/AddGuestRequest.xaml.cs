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
            try
            {
                GuestRequest gr = new GuestRequest()
                {

                    PrivateName = PrivateNameBox.Text,
                    FamilyName = FamilyNameBox.Text,
                    MailAddress = MailAddressBox.Text,
                    Area = convertStringToArea(AreaComboBox.SelectionBoxItem as string),
                    Type = convertStringToType(HostingUnitComboBox.SelectionBoxItem as string),
                    Adults = (int)AdultSlider.Value,
                    Children = (int)ChildSlider.Value,

                };
            }
            catch
            {

            }
        }

        private Enums.IsInterested convertStringToInterested(string str)
        {
            switch (str)
            {
                case Enums.IsInterested.Necessary.ToString():
                    return Enums.IsInterested.Necessary;
                case Enums.IsInterested.Possible.ToString():
                    return Enums.IsInterested.Possible;
                case Enums.IsInterested.Uninterested.ToString():
                    return Enums.IsInterested.Uninterested;
                default:
                    throw; //TODO: What does it throw
            }
        }

        private Enums.Area convertStringToArea(string str)
        {
            switch (str)
            {
                case Enums.Area.All.ToString():
                    return Enums.Area.All;
                case Enums.Area.North.ToString():
                    return Enums.Area.North;
                case Enums.Area.South.ToString():
                    return Enums.Area.South;
                case Enums.Area.Center.ToString():
                    return Enums.Area.Center;
                case Enums.Area.Jerusalem.ToString():
                    return Enums.Area.Jerusalem;
                default:
                    throw; //TODO: What does it throw
            }
        }

        private Enums.HostingUnitType convertStringToType(string str)
        {
            switch (str)
            {
                case Enums.HostingUnitType.All:
                    return Enums.HostingUnitType.All;
                case Enums.HostingUnitType.AccommodationApartment:
                    return Enums.HostingUnitType.AccommodationApartment;
                case Enums.HostingUnitType.Camping:
                    return Enums.HostingUnitType.Camping;
                case Enums.HostingUnitType.Hotel:
                    return Enums.HostingUnitType.Hotel;
                case Enums.HostingUnitType.Zimmer:
                    return Enums.HostingUnitType.Zimmer;
                default:
                    throw; //TODO: What does it throw
            }
        }
    }
}
