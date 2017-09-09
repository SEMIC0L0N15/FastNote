using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FastNote.Core
{
    public class NoteGroup : BaseViewModel
    {
        #region Public Properties
        public string Name { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        #endregion
    }
}
