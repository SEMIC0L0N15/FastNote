using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FastNote.Core
{
    public class NoteGroupListDesignModel : NoteGroupListViewModel
    {
        #region Static Members
        public static NoteGroupListDesignModel Instance = new NoteGroupListDesignModel();
        #endregion

        #region Constructor
        public NoteGroupListDesignModel()
            : base(new DesingNoteGroupProvider())
        {
        }
        #endregion

    }
}
