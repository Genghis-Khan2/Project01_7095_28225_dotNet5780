﻿using System;
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
using Exceptions;
using PLWPF.Guest_Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestMenu.xaml
    /// </summary>
    public partial class GuestMenu : Window
    {
        public string UserName { get; set; } = "~~~~";
        public int Key { get; set; } = -1;

        public GuestMenu(string userName, int key)
        {
            InitializeComponent();
            this.UserName = userName;
            this.Key = key;
            this.DataContext = this;
            Refresh();
        }
        private void Refresh()
        {
            RequestListBox.Items.Clear();
            try
            {
                var t1 = CreateAccount.myBL.GetAllGuestRequests().ToArray();
                var allGuestRequest = from item in CreateAccount.myBL.GetAllGuestRequests()
                                      where item.GuestKey == Key
                                      select item;
                //var allGuestRequest = CreateAccount.myBL.GetAllGuestRequestWhere((x) => ((GuestRequest)x).GuestKey == Key);

                var t = allGuestRequest.ToArray();
                foreach (var item in allGuestRequest)
                {
                    //Border b = new Border();
                    //b.Background = Brushes.LightGray;
                    //b.BorderBrush = Brushes.Black;
                    //b.BorderThickness = new Thickness(1);
                    //GuestRequestUC uc = new GuestRequestUC(this, item);
                    //b.Child = uc;
                    //UCStackPanel.Children.Add(b);
                    RequestListBox.Items.Add(item);
                }
            }
            catch (NoItemsException)
            {
                //TextBlock tb = new TextBlock();
                //tb.Text = "No request is associated with this user";
                //RequestListBox.Items.Add(tb);
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CreateGuestRequest cgr = new CreateGuestRequest(this.Key);
            cgr.Show();
            cgr.Closing += (s, args) => Refresh();

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteRequestButton_Click(object sender, RoutedEventArgs e)
        {
            System.Media.SystemSounds.Hand.Play();
            var dialogResult = MessageBox.Show("Are you sure you want to delete the request?\nNote! This will permanently delete the request and all related Orders!", "Alert!", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                if (RequestListBox.SelectedItem == null)
                    return;
                try
                {
                    BLImp.getBL().RemoveGuestRequest(((GuestRequest)RequestListBox.SelectedItem).GuestRequestKey);
                    Refresh();
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show("Please select the request from the list and try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (ChangedWhileLinkedException)
                {
                    MessageBox.Show("This request cannot be deleted because there are orders linked to it,\n accept/reject offers and try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ShowRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestListBox.SelectedItem == null)
                return;
            GuestRequestInfo gri = new GuestRequestInfo(((GuestRequest)RequestListBox.SelectedItem));
            gri.Show();
        }

    }
}
