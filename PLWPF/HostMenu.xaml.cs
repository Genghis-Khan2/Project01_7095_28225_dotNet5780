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
    /// Interaction logic for HostMenu.xaml
    /// </summary>
    public partial class HostMenu : Window
    {
        public HostMenu()
        {
            InitializeComponent();

            var li = from i in CreateAccount.myBL.GetAllHostingUnits()
                     where i + 
                     select i;

            foreach ()
        }
    }
}
