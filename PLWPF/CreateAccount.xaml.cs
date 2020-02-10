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
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void CreateGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var guestWin = new CreateGuestAccount();
            guestWin.Closed += (s, args) => Show();
            guestWin.Show();
        }

        private void CreateHostButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var guestWin = new CreateHostAccount();
            guestWin.Closed += (s, args) => Show();
            guestWin.Show();
        }
    }
}
