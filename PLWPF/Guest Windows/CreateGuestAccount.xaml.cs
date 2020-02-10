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
    /// Interaction logic for CreateGuestAccount.xaml
    /// </summary>
    public partial class CreateGuestAccount : Window
    {
        public CreateGuestAccount()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            //TODO:fix it
            //Add email address
            //Add all other guest fields
            if (Pass.Password == ConfPass.Password)
            {
                //TODO: Add regexes
                if (GlobalVars.myBL.CheckIfGuestExists(User.Text) ||
                    GlobalVars.myBL.CheckIfHostExists(User.Text))
                {
                    MessageBox.Show("There is already an account by that name!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (User.Text.ToLower() == "admin")
                {
                    MessageBox.Show("Invalid username!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Guest guest = new Guest()
                    {
                        PrivateName = privateNameBox.Text, // These errors will be fixed by adding the appropriate textboxes
                        FamilyName = familyName.Text,
                        MailAddress = mailAddress.Text,
                        GuestRequests = new List<int>()
                    };
                    GlobalVars.myBL.AddGuest(guest);
                    GlobalVars.myBL.WriteGuestToFile(User.Text, Pass.Password, guest.GuestKey);
                    MessageBox.Show("You have been registered!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Hide();
                    var createWin = new GuestMenu(guest);
                    createWin.Closed += (s, args) => Close();
                    createWin.Show();
                }
                catch (FormatException err)
                {
                    MessageBox.Show(err.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }
            //TODO: לעשות שאי שיוון בי הטקסטבוקסים יותרע בתווית
            MessageBox.Show("Password and confirmation must match!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
