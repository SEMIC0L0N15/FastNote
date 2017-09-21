namespace FastNote.Core.Message
{
    public class NoteContentChangedMessage
    {
        #region Public Properties
        public NoteItem ChangedNoteItem { get; set; }
        #endregion

        #region Contructor
        public NoteContentChangedMessage(NoteItem changedNoteItem)
        {
            ChangedNoteItem = changedNoteItem;
        }
        #endregion
    }
}
