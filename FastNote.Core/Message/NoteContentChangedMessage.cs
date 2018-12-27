namespace FastNote.Core.Message
{
    public class NoteContentChangedMessage
    {
        public NoteItem ChangedNoteItem { get; set; }

        public NoteContentChangedMessage(NoteItem changedNoteItem)
        {
            ChangedNoteItem = changedNoteItem;
        }
    }
}
