using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;
using GalaSoft.MvvmLight;

namespace FastNote
{
    public partial class NoteBoxControl : UserControl
    {
        public NoteBoxControl()
        {
            InitializeComponent();

            if (ViewModelBase.IsInDesignModeStatic)
                this.DataContext = new NoteBoxDesignModel();
            else
                this.DataContext = ViewModelLocator.NoteBoxViewModel;
        }

        private void Item_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement contentElement)
            {
                if (contentElement.TemplatedParent is ListBoxItem listBoxItem)
                    listBoxItem.IsSelected ^= true;
            }
        }

        private void Item_OnLostFocus(object sender, RoutedEventArgs e)
        {
            foreach (NoteItemViewModel item in this.ListBox.Items)
            {
                item.IsSelected = false;
            }
        }
    }
}
