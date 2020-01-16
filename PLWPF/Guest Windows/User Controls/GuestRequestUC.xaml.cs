using BE;
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
    /// Interaction logic for GuestRequestUC.xaml
    /// </summary>
    public partial class GuestRequestUC : UserControl
    {
        private GuestMenu caller;
        public GuestRequestUC(GuestMenu caller, string date, int key, Enums.RequestStatus requestStatus)
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private string StatusToImagePath(Enums.RequestStatus requestStatus)
        {
            // Open, ClosedWithDeal, CloseWithExpired 
            switch (requestStatus)
            {
                case Enums.RequestStatus.Open:
                    return "/Images/NoImageFound.png";
                default:
                    return "/Images/NoImageFound.png";
            }
        }
    }
}
