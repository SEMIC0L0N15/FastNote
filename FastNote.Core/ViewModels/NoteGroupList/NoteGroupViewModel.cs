using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroupViewModel : ViewModelBase
    {
        public NoteGroup NoteGroup { get; set; }

        public string Name
        {
            get => NoteGroup.Name;
            set => NoteGroup.Name = value;
        }

        public NoteGroupViewModel(NoteGroup noteGroup)
        {
            NoteGroup = noteGroup;
            NoteGroup.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);
        } 
        public NoteGroupViewModel() { }

    }
}
