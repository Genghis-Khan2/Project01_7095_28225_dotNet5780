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
using System.Windows.Shapes;
using System.Linq;

namespace PLWPF.Guest_Windows
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        private GuestRequest Gr;
        public OrderInfo(GuestRequest gr)
        {
            InitializeComponent();
            Gr = gr;
            var allOrder = GlobalVars.myBL.GetAllOrders();
            var relOrder = from ord in allOrder
                           where ord.GuestRequestKey == gr.GuestRequestKey
                           select ord;
            foreach (var ord in relOrder)
            {
                OrderGUinfo oigu = new OrderGUinfo(ord, gr);
                oigu.AcceptButton.Click += (s,args) =>{ this.Close(); };
                oigu.DeclineButton.Click += (s, args) => { this.Close(); };
                OrderStackPanel.Children.Add(oigu);
            }
            if(relOrder.Count()==0)
            {
                OrderStackPanel.Children.Add(new TextBlock() { Text = "No oreder releated to this request", FontSize = 20 }) ;

            }
        }
    }
}
