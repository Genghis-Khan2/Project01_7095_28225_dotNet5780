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

namespace PLWPF.Guest_Windows
{
    /// <summary>
    /// Interaction logic for GuestRequestInfo.xaml
    /// </summary>
    public partial class GuestRequestInfo : Window
    {

        private BE.GuestRequest gr;
        public GuestRequestInfo(BE.GuestRequest gr)
        {
            InitializeComponent();
            this.gr = gr;
            LoadData();
        }

        private void LoadData()
        {
            NameLab.Content = gr.Requester.PrivateName + " " + gr.Requester.FamilyName;
            MailLab.Content = gr.Requester.MailAddress;
            RegisterLab.Content = string.Format("{0}.{1}", gr.RegistrationDate.Day, gr.RegistrationDate.Month);
            DurationLab.Content = string.Format("{0}.{1} - {2}.{3}", gr.EntryDate.Day, gr.EntryDate.Month, gr.ReleaseDate.Day, gr.ReleaseDate.Month);
            AreaLab.Content = gr.Area.ToString();
            TypeLab.Content = gr.Type.ToString();
            AdultsLab.Content = gr.Adults;
            ChildrenLab.Content = gr.Children;
            PoolLab.Content = gr.Pool.ToString();
            JacuzziLab.Content = gr.Jacuzzi.ToString();
            GardenLab.Content = gr.Garden.ToString();
            ChildAttractLab.Content = gr.ChildrensAttractions.ToString();
            if (gr.Status != BE.Enums.RequestStatus.Open)
                ShowOrderButton.IsEnabled = false;
        }

        private void ShowOrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderInfo ordWin = new OrderInfo(this.gr);
            ordWin.ShowDialog();
            gr = GlobalVars.myBL.GetGuestRequest(gr.GuestRequestKey);
            ordWin.Closing += (s, args) => { LoadData(); };
        }
    }
}
