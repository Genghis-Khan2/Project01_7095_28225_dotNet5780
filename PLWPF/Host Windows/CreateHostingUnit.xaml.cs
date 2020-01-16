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
    /// Interaction logic for CreateHostingUnit.xaml
    /// </summary>
    public partial class CreateHostingUnit : Window
    {
        BE.Host host;
        public CreateHostingUnit(BE.Host host)
        {
            InitializeComponent();
            this.host = host;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            float commission;
            bool isNum = float.TryParse(Commission.Text, out commission);
            if (!isNum)
            {
                MessageBox.Show("Commission must be a decimal point number!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BE.HostingUnit unit = new BE.HostingUnit
            {
                Commission = commission,
                Area = (BE.Enums.Area)Enum.Parse(typeof(BE.Enums.Area), Area.SelectionBoxItem as string),
                Type = (BE.Enums.HostingUnitType)Enum.Parse(typeof(BE.Enums.HostingUnitType), TypeOfUnit.SelectionBoxItem as string),
                HostingUnitName = NameOfUnit.Text,
                NumberOfPlacesForAdults = (int)AmountOfAdults.Value,
                NumberOfPlacesForChildren = (int)AmountOfChildren.Value,
                IsTherePool = (bool)HasPool.IsChecked,
                IsThereJacuzzi = (bool)HasJacuzzi.IsChecked,
                IsThereGarden = (bool)HasGarden.IsChecked,
                IsThereChildrensAttractions = (bool)HasChildrenAttractions.IsChecked,
                Diary = null,
                Owner = host
            };

            try
            {
                CreateAccount.myBL.AddHostingUnit(unit);
            }
            catch
            {
                FR.FR_Imp.GetFR().SetHostingUnitKey(FR.FR_Imp.GetFR().GetHostingUnitKey() - 1);
                MessageBox.Show("Error with one or more of your fields", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("You have registered a hosting unit successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}