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
            if (Pass.Password == ConfPass.Password)
            {
                FR_Imp.getFR().WriteGuestToFile(User.Text, Pass.Password);
                MessageBox.Show("You have been registered!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBox.Show("Password and confirmation must match!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
