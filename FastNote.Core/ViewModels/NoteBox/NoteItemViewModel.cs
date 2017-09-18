using System;
using System.Runtime.Remoting.Channels;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteItemViewModel : ViewModelBase
    {
        #region Public Properties
        public NoteItem NoteItem { get; set; }
        public bool IsSelected { get; set; }
        public int? Width { get; set; } = null;

        public string Content
        {
            get => NoteItem.Content;
            set => NoteItem.Content = value;
        }

        public DateTime CreationDate
        {
            get => NoteItem.CreationDate;
            set => NoteItem.CreationDate = value;
        }
        #endregion

        #region Constructor
        public NoteItemViewModel(NoteItem noteItem)
        {
            NoteItem = noteItem;
            NoteItem.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);
        }

        public NoteItemViewModel()
        {
            
        }
        #endregion

    }
}