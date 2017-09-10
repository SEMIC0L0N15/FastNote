using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroupViewModel : ViewModelBase
    {

        #region Public Properties
        public NoteGroup NoteGroup { get; set; }
        public bool IsSelected { get; set; }

        public string Name
        {
            get => NoteGroup.Name;
            set => NoteGroup.Name = value;
        }

        public ObservableCollection<Note> Notes
        {
            get => NoteGroup.Notes;
            set => NoteGroup.Notes = value;
        }
        #endregion

        public NoteGroupViewModel(NoteGroup noteGroup = null)
        {
            NoteGroup = noteGroup ?? new NoteGroup();
            NoteGroup.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);
        }

    }
}
