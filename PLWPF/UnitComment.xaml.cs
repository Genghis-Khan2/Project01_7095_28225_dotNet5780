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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UnitComment.xaml
    /// </summary>
    public partial class UnitComment : Window
    {
        public UnitComment()
        {
            InitializeComponent();
            UnitName.ItemsSource = from i in GlobalVars.myBL.GetAllHostingUnits()
                                   select i.HostingUnitName;

            if (GlobalVars.myBL.GetAllHostingUnits().Count() != 0)
            {
                UnitName.SelectedIndex = 0;
            }

            else
            {
                CommentBox.IsReadOnly = true;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.SubmitUnitComment(CommentBox.Text, UnitName.SelectedItem as string);
            MessageBox.Show("Commented", "Success!", MessageBoxButton.OK);
            Close();
        }
    }
}
