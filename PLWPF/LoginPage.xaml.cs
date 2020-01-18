using System;
using System.Windows;
using System.Windows.Input;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostLoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        internal static string Username;

        //TODO: Thread idea is that we create a thread that will purge any expired guest requests

        public LoginPage()
        {
            InitializeComponent();

            CreateAccount.myBL.AddGuestRequest(new GuestRequest()
            {
                Adults = 2,
                Area = Enums.Area.Jerusalem,
                Children = 0,
                ChildrensAttractions = Enums.IsInterested.Uninterested,
                EntryDate = new DateTime(2019, 2, 12),
                FamilyName = "Komet",
                Garden = Enums.IsInterested.Possible,
                Jacuzzi = Enums.IsInterested.Necessary,
                MailAddress = "Snotnose@gmail.com",
                Pool = Enums.IsInterested.Necessary,
                PrivateName = "Nibba",
                RegistrationDate = DateTime.Today,
                ReleaseDate = new DateTime(2020, 2, 19),
                Status = Enums.RequestStatus.Open,
                Type = Enums.HostingUnitType.Hotel
            });


            GuestMenu g = new GuestMenu("Noam", 123);
            g.Show();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var createWin = new CreateAccount();
            createWin.Closed += (s, args) => this.Close();
            createWin.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (FR.FR_Imp.GetFR().GuestCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                Username = UserTextBox.Text;
                Hide();
                var createWin = new GuestMenu();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
            }

            else if (FR.FR_Imp.GetFR().HostCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                Username = UserTextBox.Text;
                Hide();
                var createWin = new HostMenu(new BE.Host()
                {
                    PhoneNumber = FR.FR_Imp.GetFR().GetHostPhoneNumber(Username),
                    PrivateName = FR.FR_Imp.GetFR().GetHostPrivateName(Username),
                    FamilyName = FR.FR_Imp.GetFR().GetHostFamilyName(Username),
                    MailAddress = FR.FR_Imp.GetFR().GetHostMailAddress(Username),
                    HostKey = FR.FR_Imp.GetFR().GetHostKey(Username),
                    BankBranchDetails = new BE.BankBranch()
                });
                createWin.Closed += (s, args) => Close();
                createWin.Show();
            }

            else if (FR.FR_Imp.GetFR().AdminCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                Username = "admin";
                Hide();
                var createWin = new AdminWindow();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                WrongLabel.Visibility = Visibility.Visible;
            }


        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            UserTextBox.Text = "";
            PassBox.Password = "";
        }

        private void PassBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginButton_Click(sender, new RoutedEventArgs());
        }

        private void UserTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PassBox.Focus();
                e.Handled = true;
            }
        }
    }
}
