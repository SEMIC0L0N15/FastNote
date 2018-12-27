using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FastNote.Core
{
    public class NoteGroupListDesignModel : NoteGroupListViewModel
    {
        public static NoteGroupListDesignModel Instance = new NoteGroupListDesignModel();

        public NoteGroupListDesignModel()
            : base(new DesignNoteGroupProvider())
        {
        }
    }
}
