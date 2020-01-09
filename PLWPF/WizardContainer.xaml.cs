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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for WizardContainer.xaml
    /// </summary>
    public partial class WizardContainer : Window
    {

        public WizardContainer()
        {
            InitializeComponent();
            ContainerFrame.Content = new WizardPage1();
        }

        private void Next(object sender, RoutedEventArgs e)
        {

        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            switch (pageNum.Tag)
            {
                case "0":
                    ContainerFrame.Content = new WizardPage2();
                    pageNum.Tag = "1";
                    break;
            }
        }

        private void Prev_Button_Click(object sender, RoutedEventArgs e)
        {
            switch (pageNum.Tag)
            {
                case "1":
                    ContainerFrame.Content = new WizardPage1();
                    pageNum.Tag = "0";
                    break;
            }
        }
    }
}
