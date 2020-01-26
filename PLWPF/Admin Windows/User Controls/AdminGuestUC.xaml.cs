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
using BE;

namespace PLWPF.Admin_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for AdminGuestUC.xaml
    /// </summary>
    public partial class AdminGuestUC : UserControl
    {
        public AdminGuestUC(Guest guest)
        {
            InitializeComponent();
            Key.Content = guest.GuestKey;
            Name.Content = GlobalVars.myBL.GetGuestUsername(guest.GuestKey);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.RemoveGuest(Name.Content as string);
        }
    }
}
