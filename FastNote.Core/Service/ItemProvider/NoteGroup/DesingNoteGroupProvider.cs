using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class DesingNoteGroupProvider : INoteGroupProvider
    {
        public IEnumerable<NoteGroup> GetItems()
        {
            IEnumerable<NoteGroup> items = new List<NoteGroup>()
            {
                new NoteGroup("Szybkie notatki"),
                new NoteGroup("Gry do zagrania"),
                new NoteGroup("Filmy do obejrzenia"),
                new NoteGroup("Książki do przeczytania"),
                new NoteGroup("Linki"),
                new NoteGroup("Screeny"),
                new NoteGroup("Kod"),
            };

            return items;
        }
    }
}
