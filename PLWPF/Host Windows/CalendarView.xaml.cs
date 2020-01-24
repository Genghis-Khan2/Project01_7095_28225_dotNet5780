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
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Window
    {
        public CalendarView(BE.HostingUnit hu)
        {
            InitializeComponent();
            DisplayCalendar.DisplayDateStart = DateTime.Today.Subtract(new TimeSpan(30, 0, 0, 0));
            DisplayCalendar.DisplayDateEnd = DateTime.Today.AddDays(11 * 30);

            for (int i = 0; i < 12; i++)

            {
                for (int j = 0; j < 31; j++)
                {
                    try
                    {
                        DateTime tmp = new DateTime(DateTime.Today.Year, i + 1, j + 1);
                    }

                    catch
                    {
                        continue;
                    }

                    if (hu.Diary[i, j])
                    {
                        DisplayCalendar.BlackoutDates.Add(new CalendarDateRange(new DateTime(DateTime.Today.Year, i + 1, j + 1), new DateTime(DateTime.Today.Year, i + 1, j + 1)));
                    }
                }

            }
        }
    }
}
