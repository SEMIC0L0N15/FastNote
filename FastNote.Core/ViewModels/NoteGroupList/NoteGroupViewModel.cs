using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroupViewModel : ViewModelBase
    {
        #region Public Properties
        public NoteGroup NoteGroup { get; set; }

        public string Name
        {
            get => NoteGroup.Name;
            set => NoteGroup.Name = value;
        }
        #endregion

        #region Constructor
        public NoteGroupViewModel(NoteGroup noteGroup)
        {
            NoteGroup = noteGroup;
            NoteGroup.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);
        } 
        public NoteGroupViewModel() { }
        #endregion

    }
}
