using System;
using System.Collections.Generic;
using System.Text;

namespace FastNote.Core
{
    public class NotesViewViewModel : BaseViewModel
    {
        public List<NoteItemViewModel> Items { get; set; }
        public string TypedText { get; set; }
        public NotesViewViewModel()
        {
            Items = new List<NoteItemViewModel>()
            {
                new NoteItemViewModel { Content = "Cześć" },
                new NoteItemViewModel { Content = "Witam" }
            };

            PropertyChanged += (sender, e) =>
            {
                var s = sender;
            };
        }

    }
}
