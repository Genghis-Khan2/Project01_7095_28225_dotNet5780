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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for CreateHost.xaml
    /// </summary>
    public partial class CreateHostAccount : Window
    {
        public CreateHostAccount()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Pass.Password == ConfPass.Password)
            {
                //TODO: Needs to be a check if the user exists
                if (FR.FR_Imp.GetFR().CheckIfGuestExists(User.Text) ||
                    FR.FR_Imp.GetFR().CheckIfHostExists(User.Text))
                {
                    MessageBox.Show("There is already an account by that name!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (User.Text.ToLower() == "admin")
                {
                    MessageBox.Show("Invalid username!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!PhoneNumberBox.Text.StartsWith("05") || PhoneNumberBox.Text.Length != 10)
                {
                    MessageBox.Show("Invalid phone number! Maybe you accidentally hyphenated...", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    System.Net.Mail.MailAddress ma = new System.Net.Mail.MailAddress(MailAddressBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid mail address format!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (User.Text.IndexOf(" ") != -1)
                {
                    MessageBox.Show("Invalid username! No spaces!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                FR.FR_Imp.GetFR().WriteHostToFile(User.Text, Pass.Password, BE.Configuration.HostKey, MailAddressBox.Text, PrivateNameBox.Text, FamilyNameBox.Text, PhoneNumberBox.Text);
                MessageBox.Show("You have been registered!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                Hide();
                LoginPage.UserName = User.Text;
                var createWin = new HostMenu(new BE.Host()
                {
                    MailAddress = MailAddressBox.Text,
                    PrivateName = PrivateNameBox.Text.Substring(0, 1).ToUpper() + PrivateNameBox.Text.Substring(1),
                    FamilyName = FamilyNameBox.Text.Substring(0, 1).ToUpper() + FamilyNameBox.Text.Substring(1),
                    BankAccountNumber = 0,
                    HostKey = FR.FR_Imp.GetFR().GetHostKey(User.Text),
                    PhoneNumber = PhoneNumberBox.Text,
                    BankBranchDetails = new BE.BankBranch()
                }
                                );
                createWin.Closed += (s, args) => Show();
                createWin.Show();
                return;
            }

            else
            {
                MessageBox.Show("Passwords do not match!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
