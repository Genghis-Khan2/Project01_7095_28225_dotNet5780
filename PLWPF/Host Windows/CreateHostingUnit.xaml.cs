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
        public CreateHostingUnit()
        {
            InitializeComponent();
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
            BE.HostingUnit unit = new BE.HostingUnit();

            unit.Commission = commission;
            unit.Area = (BE.Enums.Area)Enum.Parse(typeof(BE.Enums.Area), Area.SelectionBoxItem as string);
            unit.Type = (BE.Enums.HostingUnitType)Enum.Parse(typeof(BE.Enums.HostingUnitType), TypeOfUnit.SelectionBoxItem as string);
            unit.HostingUnitName = NameOfUnit.Text;
            unit.NumberOfPlacesForAdults = (int)AmountOfAdults.Value;
            unit.NumberOfPlacesForChildren = (int)AmountOfChildren.Value;
            unit.IsTherePool = (bool)HasPool.IsChecked;
            unit.IsThereJacuzzi = (bool)HasJacuzzi.IsChecked;
            unit.IsThereGarden = (bool)HasGarden.IsChecked;
            unit.IsThereChildrensAttractions = (bool)HasChildrenAttractions.IsChecked;
            unit.Diary = null;
            unit.Owner = new BE.Host()
            {
                HostKey = FR.FR_Imp.GetFR().GetHostKey(LoginPage.Username),
                BankBranchDetails = new BE.BankBranch()
            };

            try
            {
                CreateAccount.myBL.AddHostingUnit(unit);
            }
            catch
            {
                FR.FR_Imp.GetFR().SetHostingUnitKey(FR.FR_Imp.GetFR().GetHostingUnitKey() - 1);
            }
        }
    }
}