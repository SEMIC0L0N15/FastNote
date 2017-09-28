using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    #region Property
    public class NoteBoxAcceptDropBehaviorProperty :
    AttachedBehaviorProperty<NoteBoxAcceptDropBehaviorProperty, FrameworkElement>
    {
        protected override AttachedBehavior<FrameworkElement> CreateAttachedBehavior(DependencyObject d)
        {
            return new NoteBoxAcceptDropBehavior((FrameworkElement)d);
        }
    } 
    #endregion

    public class NoteBoxAcceptDropBehavior : AcceptDropBehavior
    {
        #region Constructor
        public NoteBoxAcceptDropBehavior(FrameworkElement associatedObject)
            : base(associatedObject)
        {
        } 
        #endregion

        protected override void OnDrop(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var listBox = (ListBox) sender;
            var noteBoxViewModel = (NoteBoxViewModel) listBox.DataContext;

            noteBoxViewModel.AddNote((NoteItem) ViewModelLocator.ApplicationViewModel.DraggingObject);
        }
    }
}
