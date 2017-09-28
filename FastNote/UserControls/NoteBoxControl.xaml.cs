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
        #region Public Properties
        public NoteBoxViewModel ViewModel => (NoteBoxViewModel) DataContext;

        public InitDragAndDropBehavior DragAndDropBehavior =>
            (InitDragAndDropBehavior) InitDragAndDropBehaviorProperty.BehaviorInstances[this];

        public RichSelectionBehavior SelectionBehavior =>
            (RichSelectionBehavior) RichSelectionBehaviorProperty.BehaviorInstances[this];
        #endregion

        #region Constructor
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
        #endregion

        #region Event Handlers
        private void NoteBoxControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBox.Focus();
        }

        private void Background_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectionBehavior.DeselectAndDiscardEditAllItems();
        }
        #endregion        

        #region Implemented Interfaces

        #region IDragSource Members
        public FrameworkElement GetDraggableTile()
        {
            return DraggableTile;
        }
        #endregion

        #region RichItemsControl Members
        public ItemsControl GetItemsControl()
        {
            return ListBox;
        }
        #endregion

        #region IItemsKeeper Members
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
        #endregion

        #region IHasBackground Members
        public FrameworkElement GetBackground()
        {
            return BackgroundGrid;
        }
        #endregion 

        #endregion
    }
}
