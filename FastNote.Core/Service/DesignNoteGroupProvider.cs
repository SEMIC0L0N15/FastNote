using System;
using System.Collections.Generic;

namespace FastNote.Core
{
    public class DesignNoteGroupProvider : IItemsProvider<NoteGroupViewModel>
    {
        public IEnumerable<NoteGroupViewModel> GetItems()
        {
            IEnumerable<NoteGroupViewModel> items = new List<NoteGroupViewModel>()
            {
                new NoteGroupViewModel { Name = "Szybkie notatki" },
                new NoteGroupViewModel { Name = "Gry do zagrania" },
                new NoteGroupViewModel { Name = "Filmy do obejrzenia" },
                new NoteGroupViewModel { Name = "Linki" },
                new NoteGroupViewModel { Name = "Screeny" },
            };

            return items;
        }
    }
}
