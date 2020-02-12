using System;
using System.Collections.Generic;
using System.Net.Mail;
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
using Exceptions;

namespace PLWPF.Guest_Windows
{
    /// <summary>
    /// Interaction logic for OrderGUinfo.xaml
    /// </summary>
    public partial class OrderGUinfo : UserControl
    {
        private Order ord;
        private GuestRequest gr;
        public OrderGUinfo(Order ord, GuestRequest gr)
        {
            InitializeComponent();
            this.ord = ord;
            this.gr = gr;
            hostintUnitNameTextBlock.Text = GlobalVars.myBL.GetHostingUnit(ord.HostingUnitKey).HostingUnitName;
            keyTextBlock.Text = ord.OrderKey.ToString();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to accept this offer?\nNote! This cannot be undone afterwards", "Attention", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    GlobalVars.myBL.UpdateGuestRequestStatus(gr.GuestRequestKey, Enums.RequestStatus.ClosedWithDeal);
                    GlobalVars.myBL.UpdateOrderStatus(ord.OrderKey, Enums.OrderStatus.ClosedByCustomerResponsiveness);
                    MessageBox.Show("Application approved, enjoy your vacation");
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show("Internal Error When placing the order, close the screens and try again");
                }
                catch (AlreadyClosedException)
                {
                    MessageBox.Show("This invitation is already closed!");
                }
                catch (UnauthorizedActionException)
                {
                    MessageBox.Show("This invitation cannot be accepted because its host has not confirmed collection");
                }
                catch (SmtpException)
                {
                    MessageBox.Show("The order was accepted but there was an error sending the email");
                }
            }
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to rejected this offer?\nNote! This cannot be undone afterwards", "Attention", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    GlobalVars.myBL.UpdateOrderStatus(ord.OrderKey, Enums.OrderStatus.ClosedByCustomerUnresponsiveness);
                    MessageBox.Show("Order rejection was successful");
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show("Internal Error When placing the order, close the screens and try again");
                }
                catch (AlreadyClosedException)
                {
                    MessageBox.Show("This invitation is already closed!");
                }
                catch (UnauthorizedActionException)
                {
                    MessageBox.Show("This invitation cannot be accepted because its host has not confirmed collection");
                }
                catch (SmtpException)
                {
                    MessageBox.Show("The order was rejected but there was an error sending the email");
                }
            }
        }
    }
}