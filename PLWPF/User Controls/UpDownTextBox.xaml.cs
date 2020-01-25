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

namespace PLWPF.User_Control
{
    /// <summary>
    /// Interaction logic for UpDownTextBox.xaml
    /// </summary>
    public partial class UpDownTextBox : UserControl
    {
        private float? num = null;
        public float? Value
        {
            get
            {
                return num;
            }
            set
            {
                if (value > MaxValue)
                    num = MaxValue;
                else if (value < MinValue)
                    num = MinValue;
                else num = value;
                txtNum.Text = num == null ? "" : num.ToString();
            }
        }
        public int MinValue { get; set; }
        public UpDownTextBox()
        {
            InitializeComponent();
            MaxValue = 100;
        }
        private void CmdUp_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void CmdDown_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }
        private void TxtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null || txtNum.Text == "" || txtNum.Text == "-")
            {
                Value = null;
                return;
            }
            if (!float.TryParse(txtNum.Text, out float val))
                txtNum.Text = Value.ToString();
            else Value = val;
        }

        public int MaxValue
        {
            get
            {
                return (int)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(UpDownTextBox), new PropertyMetadata(100));
    }
}
