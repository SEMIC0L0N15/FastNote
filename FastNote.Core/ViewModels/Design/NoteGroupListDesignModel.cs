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
            : base(null)
        {
            Items = new ObservableCollection<NoteGroupViewModel>()
            {
                new NoteGroupViewModel { Name = "Szybkie notatki" },
                new NoteGroupViewModel { Name = "Gry do zagrania" },
                new NoteGroupViewModel { Name = "Filmy do obejrzenia" },
                new NoteGroupViewModel { Name = "Książki do przeczytania" },
                new NoteGroupViewModel { Name = "Linki" },
                new NoteGroupViewModel { Name = "Screeny" },
            };
        } 
        #endregion
    }
}
