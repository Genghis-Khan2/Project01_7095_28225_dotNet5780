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

namespace PLWPF.Host_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for HostOrderInfo.xaml
    /// </summary>
    public partial class HostOrderInfo : Window
    {
        public HostOrderInfo(BE.Order ord)
        {
            InitializeComponent();
            MainGrid.DataContext = ord;
            CreateDate.Content = string.Format("{0}.{1}", ord.CreateDate.Day, ord.CreateDate.Month);
            CreateDate.Content = string.Format("{0}.{1}", ord.OrderDate.Day, ord.OrderDate.Month);
        }
    }
}
