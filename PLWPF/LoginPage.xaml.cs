using System.Windows;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostLoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        internal static string Username;

        public LoginPage()
        {
            InitializeComponent();
            GuestMenu g = new GuestMenu();
            g.Show();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var createWin = new CreateAccount();
            createWin.Closed += (s, args) => this.Close();
            createWin.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (FR.FR_Imp.GetFR().GuestCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                Username = UserTextBox.Text;
                Hide();
                var createWin = new GuestMenu();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
            }

            else if (FR.FR_Imp.GetFR().HostCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                Username = UserTextBox.Text;
                Hide();
                var createWin = new HostMenu();
                createWin.Closed += (s, args) => Close();
                createWin.Show();
            }

            else if (FR.FR_Imp.GetFR().AdminCompareToPasswordInFile(UserTextBox.Text.ToLower(), PassBox.Password))
            {
                Username = "admin";
                Hide();
                var createWin = new AdminWindow();
                createWin.Closed += (s, args) => this.Close();
                createWin.Show();
            }
            else
            {
                System.Media.SystemSounds.Hand.Play();
                WrongLabel.Visibility = Visibility.Visible;
            }


        }
    }
}
