﻿using System;
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
    /// Interaction logic for Comment.xaml
    /// </summary>
    public partial class Comment : UserControl
    {
        public Comment(string comment)
        {
            InitializeComponent();
            CommentBlock.Text = comment;
        }

        private void IssueFixed_Click(object sender, RoutedEventArgs e)
        {
            string comment = CommentBlock.Text.Substring(CommentBlock.Text.IndexOf(": ") + 2);
            GlobalVars.myBL.RemoveUnitComment(comment);
        }
    }
}
