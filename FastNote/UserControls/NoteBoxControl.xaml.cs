using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using FastNote.Core;
using GalaSoft.MvvmLight;

namespace FastNote
{
    public partial class NoteBoxControl : UserControl, IDragSource, IRichItemsControl
    {
        public NoteBoxViewModel ViewModel => (NoteBoxViewModel) DataContext;

        public InitDragAndDropBehavior DragAndDropBehavior =>
            (InitDragAndDropBehavior) InitDragAndDropBehaviorProperty.BehaviorInstances[this];

        public RichSelectionBehavior SelectionBehavior =>
            (RichSelectionBehavior) RichSelectionBehaviorProperty.BehaviorInstances[this];

        public NoteBoxControl()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.NoteBoxViewModel;

            ListBox.ItemContainerGenerator.StatusChanged += (sender, e) =>
            {
                var generator = (ItemContainerGenerator) sender;
                
                if (generator.Status == GeneratorStatus.ContainersGenerated) 
                    ItemsChanged();
            };


            SetValue(InitDragAndDropBehaviorProperty.ValueProperty, true);
            SetValue(RichSelectionBehaviorProperty.ValueProperty, true);
        }

        private void NoteBoxControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox.Focus();
        }

        private void Background_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectionBehavior.DeselectAndDiscardEditAllItems();
        }


        public FrameworkElement GetDraggableTile()
        {
            return DraggableTile;
        }

        public ItemsControl GetItemsControl()
        {
            return ListBox;
        }

        public event Action ItemsChanged = () => { };

        public void AddItem(object item)
        {
            ViewModel.AddNote((NoteItem)item);
        }

        public void DeleteItem(object item)
        {
            ViewModel.DeleteNote((NoteItem)item);
        }

        public IEnumerable<FrameworkElement> GetItems()
        {
            return ViewModel.Items.Select(
                vm => (FrameworkElement) ListBox.ItemContainerGenerator.ContainerFromItem(vm)).ToList();
        }

        public FrameworkElement GetBackground()
        {
            return BackgroundGrid;
        }
    }
}
