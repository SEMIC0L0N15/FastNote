﻿using FastNote.Core;
using System.Windows;

namespace FastNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel(this);
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as FrameworkElement)?.Focus();
            noteBox.DeselectAllItems();
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                noteBox.Tag = "15 0 15 50";
            else
                noteBox.Tag = "15 0 15 10";
        }
    }
}
