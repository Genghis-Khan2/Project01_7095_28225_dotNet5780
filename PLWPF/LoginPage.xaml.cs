using System;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using BE;
using System.Linq;
using System.ComponentModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostLoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private void KillExpiredGRs(object sender, DoWorkEventArgs de)
        {
            try
            {
                var li = from i in GlobalVars.myBL.GetAllGuestRequests()
                         where i.Status == Enums.RequestStatus.CloseWithExpired
                         select i;

                foreach (var i in li)
                {
                    GlobalVars.myBL.RemoveGuestRequest(i.GuestRequestKey);
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }

            Thread.Sleep(10000);
        }

        //TODO: Thread idea is that we create a thread that will purge any expired guest requests

        public LoginPage()
        {
            InitializeComponent();
            var bw = new BackgroundWorker();
            bw.DoWork += KillExpiredGRs;
            bw.WorkerReportsProgress = false;
            bw.RunWorkerAsync();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            var createWin = new CreateAccount();
            createWin.Closed += (s, args) =>
            {
                Show();
                ClearButton_Click(sender, e);
            };
            createWin.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVars.myBL.GuestCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                GlobalVars.UserName = UserTextBox.Text;
                Hide();
                var createWin = new GuestMenu(GlobalVars.myBL.GetGuest(GlobalVars.myBL.GetGuestKey(GlobalVars.UserName)));
                createWin.Closed += (s, args) =>
                {
                    ClearButton_Click(null, null);
                    Show();
                };
                createWin.Show();
            }

            else if (GlobalVars.myBL.HostCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                GlobalVars.UserName = UserTextBox.Text;
                Hide();
                var createWin = new HostMenu(GlobalVars.myBL.GetHost(GlobalVars.myBL.GetHostKey(GlobalVars.UserName)));
                createWin.Closed += (s, args) =>
                {
                    ClearButton_Click(null, null);
                    Show();
                };
                createWin.Show();
            }

            else if (GlobalVars.myBL.AdminCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                GlobalVars.UserName = "admin";
                Hide();
                var createWin = new AdminWindow();
                createWin.Closed += (s, args) =>
                {
                    ClearButton_Click(null, null);
                    Show();
                };
                createWin.Show();
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                WrongLabel.Visibility = Visibility.Visible;
            }


        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            UserTextBox.Text = "";
            PassBox.Password = "";
        }

        private void PassBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginButton_Click(sender, new RoutedEventArgs());
        }

        private void UserTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PassBox.Focus();
                e.Handled = true;
            }
        }
    }
}
