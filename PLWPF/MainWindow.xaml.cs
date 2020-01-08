using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool wizardIsOpen = false;
        public MainWindow()
        {
            InitializeComponent();
            wizardIsOpen = false;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!wizardIsOpen)
            {
                wizardIsOpen = true;
                var wiz = new Window();
                wiz.Height = 525;
                wiz.Width = 300;
                wiz.Show();
                wiz.Content = new WizardPage1();
                wiz.Closed += wizardClose;
            }

            else
            {
                MessageBox.Show("There can only be one wizard open at a time", "There can be only one!",
                    MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void wizardClose(object sender, EventArgs e)
        {
            wizardIsOpen = false;
        }
    }
}
