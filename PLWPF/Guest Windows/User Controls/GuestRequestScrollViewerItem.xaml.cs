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
        public string Date { get { return date; } set { date = String.Format("Dates: {0}", value); } }
        private string date = null;
        public string Key { get { return key; } set { key = String.Format("Key: {0}", value); } }
        private string key = null;
        public string ImagePath { get; set; } = @"..\..\..\PLWPF\Images\NoImageFound.png";
    }
}
