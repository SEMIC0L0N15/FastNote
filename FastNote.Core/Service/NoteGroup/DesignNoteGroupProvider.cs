using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class DesignNoteGroupProvider : INoteGroupProvider
    {
        public IEnumerable<NoteGroup> GetItems(User user)
        {
            IEnumerable<NoteGroup> items = new List<NoteGroup>()
            {
                new NoteGroup("Szybkie notatki"),
                new NoteGroup("Gry do zagrania")
                {
                    Notes = new ObservableCollection<NoteItem>
                    {
                        new NoteItem("Lords of The Fallen"),
                        new NoteItem("DeusEx: Bunt Ludzkości"),
                        new NoteItem("Sid Meier's Civilization VI"),
                        new NoteItem("Orcs must die!"),
                        new NoteItem("Mass Effect: Andromeda"),
                    }
                },
                new NoteGroup("Filmy do obejrzenia")
                {
                    Notes = new ObservableCollection<NoteItem>
                    {
                        new NoteItem("Gladiator"),
                        new NoteItem("Interstellar"),
                        new NoteItem("Incepcja"),
                    }
                },
                new NoteGroup("Książki do przeczytania"),
                new NoteGroup("Linki"),
                new NoteGroup("Screeny"),
            };

            return items;
        }
    }
}
