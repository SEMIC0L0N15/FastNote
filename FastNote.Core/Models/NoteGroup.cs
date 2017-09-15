using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroup : ObservableObject
    {
        #region Public Properties
        public string Name { get; set; }
        #endregion

        #region Constructor
        public NoteGroup(string name)
        {
            Name = name;
        }
        #endregion
    }
}
