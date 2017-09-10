using System;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteItemViewModel : ViewModelBase
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSelected { get; set; }
        public int? Width { get; set; } = null;        
    }
}