using System;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteItem : ObservableObject
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }

        public NoteItem(string content)
        {
            Content = content;
            CreationDate = DateTime.Now;
        }

        public NoteItem()
        {
            CreationDate = DateTime.Now;
        }
    }
}
