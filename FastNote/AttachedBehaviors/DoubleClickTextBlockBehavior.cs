using FastNote.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    public class DoubleClickTextBlockBehaviorProperty :
    AttachedBehaviorProperty<DoubleClickTextBlockBehaviorProperty, ListBoxItem>
    {
        protected override AttachedBehavior<ListBoxItem> CreateAttachedBehavior(DependencyObject d)
        {
            return new DoubleClickTextBlockBehavior((ListBoxItem)d);
        }
    } 

    public class DoubleClickTextBlockBehavior : AttachedBehavior<ListBoxItem>
    {
        public DoubleClickTextBlockBehavior(ListBoxItem associatedObject)
            : base(associatedObject)
        {

        }

        public override void OnAttached()
        {
            AssociatedObject.MouseDoubleClick += OnMouseDown;
        }

        public override void OnDetaching()
        {
            AssociatedObject.MouseDoubleClick -= OnMouseDown;
        } 

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var associatedObject = (ListBoxItem) sender;
            ((NoteItemViewModel) associatedObject.DataContext).StartEditing();
        }
        
    }
}
