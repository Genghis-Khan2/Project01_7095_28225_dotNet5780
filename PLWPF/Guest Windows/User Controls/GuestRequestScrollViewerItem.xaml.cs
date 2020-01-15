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

namespace PLWPF.Guest_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for GuestRequestScrollViewerItem.xaml
    /// </summary>
    public partial class GuestRequestScrollViewerItem : UserControl
    {
        public GuestRequestScrollViewerItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public string Date { get; set; } = "0.0 - 0.0";
        public int Key { get; set; } = -1;
        public string ImagePath { get; set; } = @"..\..\..\PLWPF\Images\NoImageFound.png";
    }
}
