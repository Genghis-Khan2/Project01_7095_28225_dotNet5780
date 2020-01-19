using PLWPF.Admin_Windows;
using PLWPF.Host_Windows.User_Controls;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Refresh_GuestRequests()
        {
            var li = from i in CreateAccount.myBL.GetAllGuestRequests()
                     select i;
            foreach (var i in li)
            {
                GRStack.Children.Add(new AdminGuestRequestUC(i));
            }
        }

        private void Refresh_Hosts()
        {

        }

        private void Complete_Refresh()
        {

        }
    }
}
