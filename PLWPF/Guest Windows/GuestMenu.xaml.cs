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
using System.Linq;
using BL;
using BE;
using PLWPF.Guest_Windows.User_Controls;
using Exceptions;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestMenu.xaml
    /// </summary>
    public partial class GuestMenu : Window
    {
        public string UserName { get; set; } = "~~~~";
        public int Key { get; set; } = -1;
        public GuestMenu()
        {
            InitializeComponent();
        }
        public GuestMenu(string userName, int key)
        {
            InitializeComponent();
            this.UserName = userName;
            this.Key = key;
            InitScrollViewer(Key);
        }
        private void InitScrollViewer(int key)
        {
            //var allGuestRequest = BLImp.getBL().GetAllGuestRequestToGuest(key);

            try
            {
                var allGuestRequest = BLImp.getBL().GetAllGuestRequests();
                foreach (var item in allGuestRequest)
                {
                    Border b = new Border();
                    b.Background = Brushes.LightGray;
                    b.BorderBrush = Brushes.Black;
                    b.BorderThickness = new Thickness(1);
                    GuestRequestUC uc = new GuestRequestUC()
                    {
                        Date = String.Format(item.EntryDate.Day + "." + item.EntryDate.Month + " - " + item.ReleaseDate.Day + "." + item.ReleaseDate.Month),
                        Key = item.GuestRequestKey,
                    };
                    b.Child = uc;
                    UCStackPanel.Children.Add(b);
                }
            }
            catch(NoItemsException)
            {

            }
           
        }
    }
}
