using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroup : ObservableObject
    {
        #region Public Properties
        public string Name { get; set; }
        public List<NoteItem> Notes { get; set; } = new List<NoteItem>();
        #endregion

        #region Constructor
        public NoteGroup(string name)
        {
            Name = name;
        }

        public NoteGroup()
        {
            
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
