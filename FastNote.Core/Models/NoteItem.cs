using System;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteItem : ObservableObject
    {
        #region Public Properties
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        #endregion

        #region Constructor
        public NoteItem(string content)
        {
            Content = content;
        }
        #endregion
    }
}
