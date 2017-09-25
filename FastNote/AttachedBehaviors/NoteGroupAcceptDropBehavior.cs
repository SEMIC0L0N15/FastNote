using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    public class NoteGroupAcceptDropBehavior : AcceptDropBehavior<NoteGroupAcceptDropBehavior>
    {
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
