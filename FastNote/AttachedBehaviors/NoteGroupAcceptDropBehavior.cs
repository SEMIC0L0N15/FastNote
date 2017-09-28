using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    #region Property
    public class NoteGroupAcceptDropBehaviorProperty :
    AttachedBehaviorProperty<NoteGroupAcceptDropBehaviorProperty, FrameworkElement>
    {
        protected override AttachedBehavior<FrameworkElement> CreateAttachedBehavior(DependencyObject d)
        {
            return new NoteGroupAcceptDropBehavior((FrameworkElement)d);
        }
    } 
    #endregion

    public class NoteGroupAcceptDropBehavior : AcceptDropBehavior
    {
        #region Constructor
        public NoteGroupAcceptDropBehavior(FrameworkElement associatedObject)
            : base(associatedObject)
        {
        } 
        #endregion

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
