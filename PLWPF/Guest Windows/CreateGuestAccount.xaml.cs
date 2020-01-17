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
                //TODO: Needs to be a check if the user exists
                if (!(FR.FR_Imp.GetFR().GuestCompareToPasswordInFile(User.Text, Pass.Password) ||
                    FR.FR_Imp.GetFR().HostCompareToPasswordInFile(User.Text, Pass.Password) ||
                    FR.FR_Imp.GetFR().AdminCompareToPasswordInFile(User.Text, Pass.Password)))
                {
                    MessageBox.Show("There is already an account by that name!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                FR_Imp.GetFR().WriteGuestToFile(User.Text, Pass.Password, BE.Configuration.GuestKey);
                MessageBox.Show("You have been registered!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                var createWin = new GuestMenu();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
                return;
            }
            //TODO: לעשות שאי שיוון בי הטקסטבוקסים יותרע בתווית
            MessageBox.Show("Password and confirmation must match!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
