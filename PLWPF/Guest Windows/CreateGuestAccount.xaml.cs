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
using FR;

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
            if (Pass.Password == ConfPass.Password)
            {

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
                int key = Configuration.GuestKey;
                FR_Imp.GetFR().WriteGuestToFile(User.Text, Pass.Password, key);
                MessageBox.Show("You have been registered!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                var createWin = new GuestMenu(User.Text, key);
                createWin.Closed += (s, args) => Show();
                createWin.Show();
                return;
            }
            //TODO: לעשות שאי שיוון בי הטקסטבוקסים יותרע בתווית
            MessageBox.Show("Password and confirmation must match!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
