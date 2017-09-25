using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    public class NoteBoxAcceptDropBehavior : AcceptDropBehavior<NoteBoxAcceptDropBehavior>
    {
        protected override void OnDrop(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var listBox = (ListBox) sender;
            var noteBoxViewModel = (NoteBoxViewModel) listBox.DataContext;

            noteBoxViewModel.AddNote((NoteItem) ViewModelLocator.ApplicationViewModel.DraggingObject);
        }
    }
}
