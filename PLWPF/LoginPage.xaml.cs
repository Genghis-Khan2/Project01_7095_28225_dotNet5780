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
    /// Interaction logic for HostLoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var createWin = new CreateGuestAccount();
            createWin.Closed += (s, args) => this.Close();
            createWin.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (FR.FR_Imp.getFR().GuestCompareToPasswordInFile(UserTextBox.Text, PassBox.Password))
            {
                this.Hide();
                var createWin = new GuestMenu();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
            }

            if (FR.FR_Imp.getFR().AdminCompareToPasswordInFile(UserTextBox.Text, PassBox.Password))
            {
                this.Hide();
                var createWin = new AdminWindow();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
            }


        }
    }
}
