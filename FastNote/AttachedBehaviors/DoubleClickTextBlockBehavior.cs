using FastNote.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    #region Property
    public class DoubleClickTextBlockBehaviorProperty :
    AttachedBehaviorProperty<DoubleClickTextBlockBehaviorProperty, ListBoxItem>
    {
        protected override AttachedBehavior<ListBoxItem> CreateAttachedBehavior(DependencyObject d)
        {
            return new DoubleClickTextBlockBehavior((ListBoxItem)d);
        }
    } 
    #endregion

    public class DoubleClickTextBlockBehavior : AttachedBehavior<ListBoxItem>
    {
        #region Constructor
        public DoubleClickTextBlockBehavior(ListBoxItem associatedObject)
            : base(associatedObject)
        {

        }
        #endregion

        #region Attach/Detach
        public override void OnAttached()
        {
            AssociatedObject.MouseDoubleClick += OnMouseDown;
        }

        public override void OnDetaching()
        {
            AssociatedObject.MouseDoubleClick -= OnMouseDown;
        } 
        #endregion

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var associatedObject = (ListBoxItem) sender;
            ((NoteItemViewModel) associatedObject.DataContext).StartEditing();
        }
        
    }
}
