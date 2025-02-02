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
using System.Text.RegularExpressions;

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
            Regex propPhoneNumber = new Regex("^05[0-9]{8}$");
            Regex propUserName = new Regex("^[a-z]*$");
            Regex propPassWord = new Regex("^[A-Za-z0-9]*$");
            Regex propPrivateName = new Regex("^[A-Z][a-z]*$");
            Regex propFamilyName = new Regex("^[A-Z][a-z]*$");

            if (Pass.Password == ConfPass.Password)
            {
                //TODO: Needs to be a check if the user exists
                if (FR.FR_Imp.GetFR().CheckIfGuestExists(User.Text) ||
                    FR.FR_Imp.GetFR().CheckIfHostExists(User.Text))
                {
                    MessageBox.Show("There is already an account by that name!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (User.Text.ToLower() == "admin")
                {
                    MessageBox.Show("Invalid username!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!propPhoneNumber.IsMatch(PhoneNumberBox.Text))
                {
                    MessageBox.Show("Invalid phone number! Maybe you accidentally hyphenated...", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    System.Net.Mail.MailAddress ma = new System.Net.Mail.MailAddress(MailAddressBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid mail address format!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!propUserName.IsMatch(User.Text))
                {
                    MessageBox.Show("Invalid username format!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!propPassWord.IsMatch(Pass.Password))
                {
                    MessageBox.Show("Invalid password format! Password should only contain alphanumeric characters", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!propPrivateName.IsMatch(PrivateNameBox.Text))
                {
                    MessageBox.Show("Invalid private name format! Make sure to capitalize!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!propFamilyName.IsMatch(FamilyNameBox.Text))
                {
                    MessageBox.Show("Invalid family name format! Make sure to capitalize!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                FR.FR_Imp.GetFR().WriteHostToFile(User.Text, Pass.Password, BE.Configuration.HostKey, MailAddressBox.Text, PrivateNameBox.Text, FamilyNameBox.Text, PhoneNumberBox.Text);
                MessageBox.Show("You have been registered!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                Hide();
                LoginPage.UserName = User.Text;
                var createWin = new HostMenu(new BE.Host()
                {
                    MailAddress = MailAddressBox.Text,
                    PrivateName = PrivateNameBox.Text.Substring(0, 1).ToUpper() + PrivateNameBox.Text.Substring(1),
                    FamilyName = FamilyNameBox.Text.Substring(0, 1).ToUpper() + FamilyNameBox.Text.Substring(1),
                    BankAccountNumber = 0,
                    HostKey = FR.FR_Imp.GetFR().GetHostKey(User.Text),
                    PhoneNumber = PhoneNumberBox.Text,
                    BankBranchDetails = new BE.BankBranch()
                }
                                );
                createWin.Closed += (s, args) => Close();
                createWin.Show();
                return;
            }

            else
            {
                MessageBox.Show("Passwords do not match!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
