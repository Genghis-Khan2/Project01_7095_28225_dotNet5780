using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF.Host_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for ReadioButtonSelectHU.xaml
    /// </summary>
    public partial class RadioButtonSelectHU : Window
    {
        private BE.Host host;
        private BE.GuestRequest gr;
        public RadioButtonSelectHU(BE.GuestRequest gr, BE.Host host)
        {
            InitializeComponent();
            this.gr = gr;
            this.host = host;
            AddRadioButtons();
        }

        private void AddRadioButtons()
        {
            foreach (var i in GlobalVars.myBL.GetMatchingHostingUnits(gr, host))
            {
                RadioButton rb = new RadioButton();
                rb.Content = i.HostingUnitName;
                RadioButtons.Items.Add(rb);
            }
        }

        public BE.HostingUnit GetSelection()
        {
            foreach (var i in RadioButtons.Items)
            {
                if (i is RadioButton)
                {
                    RadioButton rb = i as RadioButton;
                    if ((bool)rb.IsChecked)
                    {
                        return GlobalVars.myBL.GetMatchingHostingUnits(gr, host)[RadioButtons.Items.IndexOf(rb)];
                    }
                }
            }

            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Are you sure you've selected the correct hosting unit? This action cannot be undone.", "Make Sure", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (answer == MessageBoxResult.Cancel)
            {
                return;
            }

            Close();
        }
    }
}
