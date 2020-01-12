using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static BL.IBL myBL = BL.BLImp.getBL();


        private static bool wizardIsOpen = false;
        public MainWindow()
        {
            BE.GuestRequest gr = new BE.GuestRequest()
            {
                Adults = 1,
                Children = 0,
                Area = BE.Enums.Area.All,
                ChildrensAttractions = BE.Enums.IsInterested.Uninterested,
                EntryDate = new DateTime(2019, 5, 6),
                FamilyName = "Komet",
                Garden = BE.Enums.IsInterested.Uninterested,
                Jacuzzi = BE.Enums.IsInterested.Uninterested,
                MailAddress = "a@g.c",
                Pool = BE.Enums.IsInterested.Uninterested,
                PrivateName = "Gabi",
                RegistrationDate = new DateTime(2019, 4, 20),
                ReleaseDate = new DateTime(2019, 6, 1),
                Type = BE.Enums.HostingUnitType.Hotel
            };

            BE.GuestRequest gr2 = new BE.GuestRequest()
            {
                Adults = 1,
                Children = 0,
                Area = BE.Enums.Area.All,
                ChildrensAttractions = BE.Enums.IsInterested.Uninterested,
                EntryDate = new DateTime(2019, 5, 6),
                FamilyName = "Snuggum",
                Garden = BE.Enums.IsInterested.Uninterested,
                Jacuzzi = BE.Enums.IsInterested.Uninterested,
                MailAddress = "b@kfc.com",
                Pool = BE.Enums.IsInterested.Uninterested,
                PrivateName = "Ethan",
                RegistrationDate = new DateTime(2019, 4, 20),
                ReleaseDate = new DateTime(2019, 6, 1),
                Type = BE.Enums.HostingUnitType.Hotel
            };

            myBL.AddGuestRequest(gr);
            myBL.AddGuestRequest(gr2);
            Application.Current.Shutdown();
            InitializeComponent();
            wizardIsOpen = false;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!wizardIsOpen)
            {
                wizardIsOpen = true;
                var wiz = new WizardContainer
                {
                    Title = "Wizard"
                };
                wiz.Show();
                wiz.Closed += WizardClose;
            }

            else
            {
                MessageBox.Show("There can only be one wizard open at a time", "There can be only one!",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void WizardClose(object sender, EventArgs e)
        {
            wizardIsOpen = false;
        }

        private void GuestLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var guestWin = new AddGuestRequest();
            guestWin.Closed += (s, args) => this.Show();
            guestWin.Show();
        }
    }
}
