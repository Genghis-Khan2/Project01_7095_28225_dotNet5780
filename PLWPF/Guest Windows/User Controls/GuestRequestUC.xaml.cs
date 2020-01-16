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
using BL;
namespace PLWPF.Guest_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for GuestRequestUC.xaml
    /// </summary>
    public partial class GuestRequestUC : UserControl
    {
        private GuestMenu caller;
        private GuestRequest gr;
        public GuestRequestUC(GuestMenu caller, GuestRequest guestRequest)
        {
            InitializeComponent();
            this.caller = caller;
            this.gr = guestRequest;
            this.KeyTextBlock.Text = gr.GuestRequestKey.ToString();
            this.DateTextBlock.Text = String.Format("{0}.{1} - {2}.{3}", guestRequest.EntryDate.Day, guestRequest.EntryDate.Month, guestRequest.ReleaseDate.Day, guestRequest.ReleaseDate.Month);
            this.StatusIconImage.Source = new BitmapImage(new Uri(StatusToImagePath(guestRequest.Status)));
        }
        private string StatusToImagePath(Enums.RequestStatus requestStatus)
        {
            // Open, ClosedWithDeal, CloseWithExpired 
            switch (requestStatus)
            {
                case Enums.RequestStatus.Open:
                    return "/Images/InProgressIcon.png";
                case Enums.RequestStatus.ClosedWithDeal:
                    return "/Images/ViIcon.png";
                case Enums.RequestStatus.CloseWithExpired:
                    return "/Images/NoIcon.png";
            }
            return "/Images/NoImageFound.png";
        }
        private void RemoveButtonUC_Click(object sender, RoutedEventArgs e)
        {
            caller.RemoveGuestRequest(gr.GuestRequestKey);
            //TODO:do it
        }

        private void EditButtonUC_Click(object sender, RoutedEventArgs e)
        {
            caller.EditGuestRequest(gr.GuestRequestKey);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO: זה כאשר הוא לוחץ על בקשת אורח מסויימת, אמור לפתוח חלון חדש
        }
    }
}
