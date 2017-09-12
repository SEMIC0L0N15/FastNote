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
            : base(new DummyNoteGroupProvider())
        {
        }
        #endregion



        private class DummyNoteGroupProvider : INoteGroupProvider
        {

            public IEnumerable<NoteGroup> GetItems(User parameter)
            {
                var items = new List<NoteGroup>()
            {
                new NoteGroup("Szybkie notatki"),
                new NoteGroup("Gry do zagrania"),
                new NoteGroup("Filmy do obejrzenia"),
                new NoteGroup("Książki do przeczytania"),
                new NoteGroup("Linki"),
                new NoteGroup("Screeny"),
            };

                return items;
            }
        }
    }
}
