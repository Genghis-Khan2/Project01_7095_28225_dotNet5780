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

namespace PLWPF.Host_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for GuestRequestInfo.xaml
    /// </summary>
    public partial class GuestRequestInfo : Window
    {
        private BE.GuestRequest gr;
        private BE.Host host;
        private BE.HostingUnit huOrderedTo = null;
        public GuestRequestInfo(BE.GuestRequest gr, BE.Host host)
        {
            InitializeComponent();
            this.gr = gr;
            this.host = host;
            LoadData();
        }

        private void LoadData()
        {
            NameLab.Content = gr.Requester.PrivateName + " " + gr.Requester.FamilyName;
            MailLab.Content = gr.Requester.MailAddress;
            RegisterLab.Content = string.Format("{0}.{1}", gr.RegistrationDate.Day, gr.RegistrationDate.Month);
            DurationLab.Content = string.Format("{0}.{1} - {2}.{3}", gr.EntryDate.Day, gr.EntryDate.Month, gr.ReleaseDate.Day, gr.ReleaseDate.Month);
            AreaLab.Content = gr.Area.ToString();
            TypeLab.Content = gr.Type.ToString();
            AdultsLab.Content = gr.Adults;
            ChildrenLab.Content = gr.Children;
            PoolLab.Content = gr.Pool.ToString();
            JacuzziLab.Content = gr.Jacuzzi.ToString();
            GardenLab.Content = gr.Garden.ToString();
            ChildAttractLab.Content = gr.ChildrensAttractions.ToString();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var radio = new RadioButtonSelectHU(gr, host);
            BE.HostingUnit hu = new BE.HostingUnit();
            radio.Closing += (s, args) => hu = radio.GetSelection();
            radio.ShowDialog();
            BE.Order ord = new BE.Order();
            if (hu != null)
            {
                try
                {
                    ord = new BE.Order()
                    {
                        CreateDate = DateTime.Today,
                        GuestRequestKey = gr.GuestRequestKey,
                        HostingUnitKey = hu.HostingUnitKey,//Give to select from
                        OrderDate = DateTime.Today,
                        Status = BE.Enums.OrderStatus.UnTreated
                    };

                    GlobalVars.myBL.AddOrder(ord);
                    hu.Diary = GlobalVars.myBL.MarkingInTheDiary(hu, gr.EntryDate, gr.ReleaseDate);
                }
                catch (Exceptions.NoItemsException)
                {
                    throw;
                }

                huOrderedTo = hu;

            }
        }

        internal BE.HostingUnit GetHostingUnit()
        {
            return huOrderedTo;
        }

    }
}
