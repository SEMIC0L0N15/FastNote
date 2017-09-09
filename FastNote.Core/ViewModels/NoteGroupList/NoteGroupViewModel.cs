using System.Collections.ObjectModel;

namespace FastNote.Core
{
    public class NoteGroupViewModel : BaseViewModel
    {
        #region Private Members
        private NoteGroup mNoteGroup;
        #endregion

        #region Public Properties
        public string Name
        {
            get => mNoteGroup.Name;
            set => mNoteGroup.Name = value;
        }

        public ObservableCollection<Note> Notes
        {
            get => mNoteGroup.Notes;
            set => mNoteGroup.Notes = value;
        }
        #endregion

        public NoteGroupViewModel(NoteGroup noteGroup = null)
        {
            mNoteGroup = noteGroup ?? new NoteGroup();
            mNoteGroup.PropertyChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
        }

    }
}
