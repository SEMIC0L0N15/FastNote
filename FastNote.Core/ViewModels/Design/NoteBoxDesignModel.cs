
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace FastNote.Core
{
    public class NoteBoxDesignModel : NoteBoxViewModel
    {
        #region Static Members
        public static NoteBoxDesignModel Instance = new NoteBoxDesignModel();
        #endregion

        #region Constructor
        public NoteBoxDesignModel()
            : base(new DesignNoteItemProvider())
        {
        }
        #endregion
    }
}
