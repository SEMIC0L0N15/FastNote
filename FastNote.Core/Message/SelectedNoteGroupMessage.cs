namespace FastNote.Core
{
    public class SelectedNoteGroupMessage
    {
        public NoteGroup SelectedGroup { get; set; }

        public SelectedNoteGroupMessage(NoteGroup noteGroup)
        {
            SelectedGroup = noteGroup;
        }
    }
}
