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
        internal static BL.IBL myBL = BL.BLImp.getBL();

        public CreateAccount()
        {
            InitializeComponent();
        }

        private void CreateGuestButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var guestWin = new CreateGuestRequest();
            guestWin.Closed += (s, args) => this.Close();
            guestWin.Show();
        }
    }
}
