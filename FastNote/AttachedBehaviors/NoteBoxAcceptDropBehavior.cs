using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    public class NoteBoxAcceptDropBehaviorProperty :
    AttachedBehaviorProperty<NoteBoxAcceptDropBehaviorProperty, FrameworkElement>
    {
        protected override AttachedBehavior<FrameworkElement> CreateAttachedBehavior(DependencyObject d)
        {
            return new NoteBoxAcceptDropBehavior((FrameworkElement)d);
        }
    } 

    public class NoteBoxAcceptDropBehavior : AcceptDropBehavior
    {
        public NoteBoxAcceptDropBehavior(FrameworkElement associatedObject)
            : base(associatedObject)
        {
        } 

        protected override void OnDrop(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var listBox = (ListBox) sender;
            var noteBoxViewModel = (NoteBoxViewModel) listBox.DataContext;

            noteBoxViewModel.AddNote((NoteItem) ViewModelLocator.ApplicationViewModel.DraggingObject);
        }
    }
}
