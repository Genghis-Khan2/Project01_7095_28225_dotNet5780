using System;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using BE;
using System.Linq;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostLoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        internal static string UserName;

        private void KillExpiredGRs()
        {
            var li = from i in CreateAccount.myBL.GetAllGuestRequests()
                     where i.Status == Enums.RequestStatus.CloseWithExpired
                     select i;

            foreach (var i in li)
            {
                CreateAccount.myBL.RemoveGuestRequest(i.GuestRequestKey);
            }

            Thread.Sleep(10000);
        }

        //TODO: Thread idea is that we create a thread that will purge any expired guest requests

        public LoginPage()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var createWin = new CreateAccount();
            createWin.Closed += (s, args) => Show();
            createWin.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (FR.FR_Imp.GetFR().GuestCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                UserName = UserTextBox.Text;
                Hide();
                var createWin = new GuestMenu(UserName, FR.FR_Imp.GetFR().GetGuestKey(UserName));
                createWin.Closed += (s, args) =>
                {
                    ClearButton_Click(null, null);
                    Show();
                };
                createWin.Show();
            }

            else if (FR.FR_Imp.GetFR().HostCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                UserName = UserTextBox.Text;
                Hide();
                var createWin = new HostMenu(new BE.Host()
                {
                    PhoneNumber = FR.FR_Imp.GetFR().GetHostPhoneNumber(UserName),
                    PrivateName = FR.FR_Imp.GetFR().GetHostPrivateName(UserName),
                    FamilyName = FR.FR_Imp.GetFR().GetHostFamilyName(UserName),
                    MailAddress = FR.FR_Imp.GetFR().GetHostMailAddress(UserName),
                    HostKey = FR.FR_Imp.GetFR().GetHostKey(UserName),
                    BankBranchDetails = new BE.BankBranch()
                });
                createWin.Closed += (s, args) =>
                {
                    ClearButton_Click(null, null);
                    Show();
                };
                createWin.Show();
            }

            else if (FR.FR_Imp.GetFR().AdminCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                UserName = "admin";
                Hide();
                var createWin = new AdminWindow();
                createWin.Closed += (s, args) =>
                {
                    ClearButton_Click(null, null);
                    Show();
                };
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
