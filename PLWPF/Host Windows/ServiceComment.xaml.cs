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
        public ServiceComment()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.SubmitHostComment(CommentBox.Text);
            MessageBox.Show("Commented", "Success!", MessageBoxButton.OK);
            Close();
        }
    }
}
