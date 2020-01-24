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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FR;

namespace PLWPF.Admin_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for AdminGuestUC.xaml
    /// </summary>
    public partial class AdminGuestUC : UserControl
    {
        public AdminGuestUC(string username)
        {
            InitializeComponent();
            Key.Content = FR_Imp.GetFR().GetGuestKey(username);
            Name.Content = username;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FR_Imp.GetFR().RemoveGuestFromFile(Name.Content as string);
        }
    }
}
