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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for CreateHost.xaml
    /// </summary>
    public partial class CreateHostAccount : Window
    {
        public CreateHostAccount()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Pass.Password == ConfPass.Password)
            {
                //TODO: Needs to be a check if the user exists
                if (!(FR.FR_Imp.GetFR().CheckIfGuestExists(User.Text) ||
                    FR.FR_Imp.GetFR().CheckIfHostExists(User.Text) ||
                    User.Text.ToLower() == "admin"))
                {
                    MessageBox.Show("There is already an account by that name!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                FR.FR_Imp.GetFR().WriteHostToFile(User.Text, Pass.Password, BE.Configuration.HostKey);
                MessageBox.Show("You have been registered!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Hide();
                var createWin = new HostMenu();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
                return;
            }
        }
    }
}
