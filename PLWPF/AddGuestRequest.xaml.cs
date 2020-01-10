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
                JacuzziComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                GardenComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                ChildrenAttractionsComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            PoolComboBox.SelectedIndex = 1;
            JacuzziComboBox.SelectedIndex = 1;
            GardenComboBox.SelectedIndex = 1;
            ChildrenAttractionsComboBox.SelectedIndex = 1;

            var hostingUnitType = Enum.GetValues(typeof(Enums.HostingUnitType));
            foreach (var item in hostingUnitType)
            {
                HostingUnitComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            HostingUnitComboBox.SelectedIndex = 0;

            ArrivalDateCalendar.SelectedDate = DateTime.Today;
            DepartureDateCalendar.SelectedDate = DateTime.Today;
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
                    Area = ConvertStringToArea(AreaComboBox.SelectionBoxItem as string),
                    Type = ConvertStringToType(HostingUnitComboBox.SelectionBoxItem as string),
                    Adults = (int)AdultSlider.Value,
                    Children = (int)ChildSlider.Value,
                    Pool = ConvertStringToInterested(PoolComboBox.SelectionBoxItem as string),
                    Jacuzzi = ConvertStringToInterested(JacuzziComboBox.SelectionBoxItem as string),
                    Garden = ConvertStringToInterested(GardenComboBox.SelectionBoxItem as string),
                    ChildrensAttractions = ConvertStringToInterested(ChildrenAttractionsComboBox.SelectionBoxItem as string),
                    EntryDate = (DateTime)ArrivalDateCalendar.SelectedDate,
                    ReleaseDate = (DateTime)DepartureDateCalendar.SelectedDate,
                    RegistrationDate = DateTime.Today
                };

                MainWindow.myBL.AddGuestRequest(gr);
            }
            catch
            {

            }
        }

        private Enums.IsInterested ConvertStringToInterested(string str)
        {
            return str switch
            {
                Enums.IsInterested.Necessary.ToString() => Enums.IsInterested.Necessary,
                Enums.IsInterested.Possible.ToString() => Enums.IsInterested.Possible,
                Enums.IsInterested.Uninterested.ToString() => Enums.IsInterested.Uninterested,
                _ => Enums.IsInterested.Necessary, //TODO: What does it throw
            };
        }

        private Enums.Area ConvertStringToArea(string str)
        {
            return str switch
            {
                Enums.Area.All.ToString() => Enums.Area.All,
                Enums.Area.North.ToString() => Enums.Area.North,
                Enums.Area.South.ToString() => Enums.Area.South,
                Enums.Area.Center.ToString() => Enums.Area.Center,
                Enums.Area.Jerusalem.ToString() => Enums.Area.Jerusalem,
                _ => Enums.Area.All, //TODO: What does it throw
            };
        }

        private Enums.HostingUnitType ConvertStringToType(string str)
        {
            return str switch
            {
                Enums.HostingUnitType.All.ToString() => Enums.HostingUnitType.All,
                Enums.HostingUnitType.AccommodationApartment.ToString() => Enums.HostingUnitType.AccommodationApartment,
                Enums.HostingUnitType.Camping.ToString() => Enums.HostingUnitType.Camping,
                Enums.HostingUnitType.Hotel.ToString() => Enums.HostingUnitType.Hotel,
                Enums.HostingUnitType.Zimmer.ToString() => Enums.HostingUnitType.Zimmer,
                _ => Enums.HostingUnitType.AccommodationApartment, //TODO: What does it throw
            };
        }
    }
}
