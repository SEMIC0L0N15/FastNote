using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroup : ObservableObject
    {
        #region Public Properties
        public string Name { get; set; }
        public ObservableCollection<NoteItem> Notes { get; set; } = 
            new ObservableCollection<NoteItem>();
        #endregion

        #region Constructor
        public NoteGroup(string name)
        {
            Name = name;
        }
        #endregion

        #region Methods
        public void AddNote(NoteItem noteItem)
        {
            Notes.Add(noteItem);
        }
        #endregion
    }
}
