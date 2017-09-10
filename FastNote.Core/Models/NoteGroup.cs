using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroup : ObservableObject
    {
        #region Public Properties
        public string Name { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        #endregion
    }
}
