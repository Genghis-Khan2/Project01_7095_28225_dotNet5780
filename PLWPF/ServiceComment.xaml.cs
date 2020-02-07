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

namespace PLWPF.Host_Windows
{
    /// <summary>
    /// Interaction logic for ServiceComment.xaml
    /// </summary>
    public partial class ServiceComment : Window
    {
        private readonly bool host;
        public ServiceComment(bool host = true)
        {
            InitializeComponent();
            this.host = host;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (host)
            {
                GlobalVars.myBL.SubmitHostComment(CommentBox.Text);
            }
            else
            {
                GlobalVars.myBL.SubmitGuestComment(CommentBox.Text);
            }

            MessageBox.Show("Commented", "Success!", MessageBoxButton.OK);
            Close();
        }
    }
}
