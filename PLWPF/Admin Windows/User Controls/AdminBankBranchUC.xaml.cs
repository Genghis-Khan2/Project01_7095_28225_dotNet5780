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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF.Admin_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for AdminBankBranchUC.xaml
    /// </summary>
    public partial class AdminBankBranchUC : UserControl
    {
        public AdminBankBranchUC(BE.BankBranch bb)
        {
            InitializeComponent();
            BankNumber.Content = bb.BankNumber;
            BankAccountNumber.Content = bb.BankAccountNumber;
            BranchNumber.Content = bb.BranchNumber;
            FullName.Content = string.Format("{0} - {1}, {2}", bb.BankName, bb.BranchAddress, bb.BranchCity);
        }
    }
}
