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
using System.Text.RegularExpressions;

namespace PLWPF.Host_Windows
{
    /// <summary>
    /// Interaction logic for CreateHostingUnit.xaml
    /// </summary>
    public partial class CreateHostingUnit : Window
    {
        private readonly BE.Host host;

        private readonly bool updating;

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
                Area.SelectedIndex = GetIndex(hu.Area);
                TypeOfUnit.SelectedIndex = GetIndex(hu.Type);
                AmountOfAdults.Value = hu.NumberOfPlacesForAdults;
                AmountOfChildren.Value = hu.NumberOfPlacesForChildren;
                HasPool.IsChecked = hu.IsTherePool;
                HasJacuzzi.IsChecked = hu.IsThereJacuzzi;
                HasGarden.IsChecked = hu.IsThereGarden;
                HasChildrenAttractions.IsChecked = hu.IsThereChildrensAttractions;
                CreateButton.Content = "Update Hosting Unit";

                KeyTextBox.Visibility = Visibility.Visible;
                Closing += (s, args) => KeyTextBox.Visibility = Visibility.Hidden;
            }
        }

        private int GetIndex(BE.Enums.Area area)
        {
            switch (area)
            {
                case BE.Enums.Area.All:
                    return 0;
                case BE.Enums.Area.North:
                    return 1;
                case BE.Enums.Area.South:
                    return 2;
                case BE.Enums.Area.Center:
                    return 3;
                case BE.Enums.Area.Jerusalem:
                    return 4;
                default:
                    return 5;
            }
        }

        private int GetIndex(BE.Enums.HostingUnitType type)
        {
            switch (type)
            {
                case BE.Enums.HostingUnitType.All:
                    return 0;
                case BE.Enums.HostingUnitType.Zimmer:
                    return 1;
                case BE.Enums.HostingUnitType.AccommodationApartment:
                    return 2;
                case BE.Enums.HostingUnitType.Hotel:
                    return 3;
                case BE.Enums.HostingUnitType.Camping:
                    return 4;
                default:
                    return 5;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Regex propHUName = new Regex(@"^(?:[A-Z][a-z]+\s?)+$");

            if (!propHUName.IsMatch(NameOfUnit.Text))
            {
                MessageBox.Show("Invalid format of name! Name must be title case!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
                    GlobalVars.myBL.RemoveHostingUnit(hu.HostingUnitKey);
                }
                GlobalVars.myBL.AddHostingUnit(unit);
            }
            catch (Exceptions.AlreadyExistsException)
            {
                MessageBox.Show("This unit already exists", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}