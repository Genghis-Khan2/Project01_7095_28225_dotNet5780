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
        private BE.Host host;

        private bool updating;

        private BE.HostingUnit hu;
        public CreateHostingUnit(BE.Host host, bool update = false, BE.HostingUnit hu = null)
        {
            InitializeComponent();
            this.host = host;
            updating = update;
            this.hu = hu;
            if (update)
            {
                NameOfUnit.Text = hu.HostingUnitName;
                Area.SelectedItem = hu.Area;
                TypeOfUnit.SelectedItem = hu.Type;
                AmountOfAdults.Value = hu.NumberOfPlacesForAdults;
                AmountOfChildren.Value = hu.NumberOfPlacesForChildren;
                HasPool.IsChecked = hu.IsTherePool;
                HasJacuzzi.IsChecked = hu.IsThereJacuzzi;
                HasGarden.IsChecked = hu.IsThereGarden;
                HasChildrenAttractions.IsChecked = hu.IsThereChildrensAttractions;
                CreateButton.Content = "Update Hosting Unit";
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            BE.HostingUnit unit = new BE.HostingUnit
            {
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

            if (updating)
            {
                unit.HostingUnitKey = hu.HostingUnitKey;
            }

            try
            {
                if (updating)
                {
                    CreateAccount.myBL.RemoveHostingUnit(hu.HostingUnitKey);
                }
                CreateAccount.myBL.AddHostingUnit(unit);
            }
            catch
            {
                FR.FR_Imp.GetFR().SetHostingUnitKey(FR.FR_Imp.GetFR().GetHostingUnitKey() - 1);
                MessageBox.Show("Error with one or more of your fields", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (updating)
            {
                MessageBox.Show("You have updated a hosting unit successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);

            }

            else
            {
                MessageBox.Show("You have registered a hosting unit successfully!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            hu = unit;

            Close();
        }

        internal BE.HostingUnit GetHostingUnit()
        {
            return hu;
        }
    }
}