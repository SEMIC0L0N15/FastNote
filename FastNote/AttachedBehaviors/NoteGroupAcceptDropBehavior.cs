using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    public class NoteGroupAcceptDropBehaviorProperty :
    AttachedBehaviorProperty<NoteGroupAcceptDropBehaviorProperty, FrameworkElement>
    {
        protected override AttachedBehavior<FrameworkElement> CreateAttachedBehavior(DependencyObject d)
        {
            return new NoteGroupAcceptDropBehavior((FrameworkElement)d);
        }
    } 

    public class NoteGroupAcceptDropBehavior : AcceptDropBehavior
    {
        public NoteGroupAcceptDropBehavior(FrameworkElement associatedObject)
            : base(associatedObject)
        {
        } 

        protected override void OnDrop(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var listBoxItem = (ListBoxItem) sender;
            var noteGroupViewModel = (NoteGroupViewModel) listBoxItem.Content;

            if (ViewModelLocator.ApplicationViewModel.DraggingObject is NoteItem noteItem)
            {
                noteGroupViewModel.NoteGroup.AddNote(noteItem);
            }
        }

    }
}
