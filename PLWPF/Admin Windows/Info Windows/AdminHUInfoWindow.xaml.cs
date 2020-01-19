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
using BE;
using PLWPF.Host_Windows;

namespace PLWPF.Admin_Windows.Info_Windows
{
    /// <summary>
    /// Interaction logic for AdminHUInfoWindow.xaml
    /// </summary>
    public partial class AdminHUInfoWindow : Window
    {
        private readonly HostingUnit hu;

        public AdminHUInfoWindow(HostingUnit hu)
        {
            InitializeComponent();
            this.hu = hu;
            LoadData(hu);
        }

        internal void LoadData(HostingUnit hu)
        {
            KeyLab.Content = hu.HostingUnitKey.ToString();
            OwnerLab.Content = hu.Owner.PrivateName + " " + hu.Owner.FamilyName;
            HostingUnitNameLab.Content = hu.HostingUnitName;
            AreaLab.Content = hu.Area.ToString();
            TypeLab.Content = hu.Type.ToString();
            AdultsLab.Content = hu.NumberOfPlacesForAdults;
            ChildrenLab.Content = hu.NumberOfPlacesForChildren;

            IsPool.Content = hu.IsTherePool;
            IsJacuzzi.Content = hu.IsThereJacuzzi;
            IsGarden.Content = hu.IsThereGarden;
            IsChildAttract.Content = hu.IsThereChildrensAttractions;
        }

        private void Diarylab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var win = new CalendarView(hu);
            win.Show();
        }
    }
}
