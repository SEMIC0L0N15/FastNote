using System;

namespace FastNote.Core
{
    public class NoteItemViewModel : BaseViewModel
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSelected { get; set; }
        public int? Width { get; set; } = null;        
    }
}