using FastNote.Core;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    public class DoubleClickTextBlockBehavior : BaseAttachedBehavior<DoubleClickTextBlockBehavior, ListBoxItem>
    {
        protected override void OnAttached(ListBoxItem associatedObject)
        {
            associatedObject.MouseDoubleClick += OnMouseDown;
        }

        protected override void OnDetaching(ListBoxItem associatedObject)
        {
            associatedObject.MouseDoubleClick -= OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var associatedObject = (ListBoxItem) sender;
            ((NoteItemViewModel) associatedObject.DataContext).StartEditing();
        }
        
    }
}
