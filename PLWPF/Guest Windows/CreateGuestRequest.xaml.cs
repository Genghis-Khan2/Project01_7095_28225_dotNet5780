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
using Exceptions;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for CreateAccountGuest.xaml
    /// </summary>
    public partial class CreateGuestRequest : Window
    {
        int GuestKey { get; set; }
        public CreateGuestRequest(int guestKey)
        {
            InitializeComponent();

            //set the value of the Area combo box using the items in the enum
            var AreaEnums = Enum.GetValues(typeof(Enums.Area));
            foreach (var item in AreaEnums)
            {
                AreaComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            //set the selected item to the first item
            AreaComboBox.SelectedIndex = 0;

            //set the value of the is interested combo box using the items in the enum
            var isInterested = Enum.GetValues(typeof(Enums.IsInterested));
            foreach (var item in isInterested)
            {
                PoolComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                JacuzziComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                GardenComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
                ChildrenAttractionsComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            //set the selected item to the first item
            PoolComboBox.SelectedIndex = 1;
            JacuzziComboBox.SelectedIndex = 1;
            GardenComboBox.SelectedIndex = 1;
            ChildrenAttractionsComboBox.SelectedIndex = 1;

            //set the value of the hosting unit type combo box using the items in the enum
            var hostingUnitType = Enum.GetValues(typeof(Enums.HostingUnitType));
            foreach (var item in hostingUnitType)
            {
                HostingUnitComboBox.Items.Add(new ComboBoxItem() { Content = item.ToString() });
            }
            //set the selected item to the first item
            HostingUnitComboBox.SelectedIndex = 0;

            ArrivalDateCalendar.SelectedDate = DateTime.Today;
            DepartureDateCalendar.SelectedDate = DateTime.Today;

            this.GuestKey = guestKey;
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
                    Area = (Enums.Area)Enum.Parse(typeof(Enums.Area),
                    AreaComboBox.SelectionBoxItem as string),

                    Type = (Enums.HostingUnitType)Enum.Parse(typeof(Enums.HostingUnitType),
                    HostingUnitComboBox.SelectionBoxItem as string),

                    Adults = (int)AdultSlider.Value,
                    Children = (int)ChildSlider.Value,

                    Pool = (Enums.IsInterested)Enum.Parse(typeof(Enums.IsInterested),
                    PoolComboBox.SelectionBoxItem as string),

                    Jacuzzi = (Enums.IsInterested)Enum.Parse(typeof(Enums.IsInterested),
                    JacuzziComboBox.SelectionBoxItem as string),

                    Garden = (Enums.IsInterested)Enum.Parse(typeof(Enums.IsInterested),
                    GardenComboBox.SelectionBoxItem as string),

                    ChildrensAttractions = (Enums.IsInterested)Enum.Parse(typeof(Enums.IsInterested),
                    ChildrenAttractionsComboBox.SelectionBoxItem as string),

                    EntryDate = (DateTime)ArrivalDateCalendar.SelectedDate,
                    ReleaseDate = (DateTime)DepartureDateCalendar.SelectedDate,
                    RegistrationDate = DateTime.Today,

                    GuestKey = this.GuestKey

                 
                };

                CreateAccount.myBL.AddGuestRequest(gr);
            }
            catch (AlreadyExistsException)
            {
                MessageBox.Show("This request already exists", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("The arrival date is later than the departure date", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Illegal Email address", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            MessageBox.Show("Your request has been received, and is beginning to be processed!", "Request Accepted", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
