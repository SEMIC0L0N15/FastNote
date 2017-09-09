using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FastNote.Core
{
    public class NoteBoxViewModel : BaseViewModel
    {
        #region Private Members
        private NoteGroup mNoteGroup;
        #endregion

        #region Public Properties
        public string TypedText { get; set; }

        public ObservableCollection<NoteItemViewModel> Items { get; set; } 
            = new ObservableCollection<NoteItemViewModel>();      
        #endregion

        #region Public Commands
        public ICommand SendNoteCommand { get; set; }
        #endregion

        #region Constructor
        public NoteBoxViewModel()
        {


            SendNoteCommand = new RelayCommand(SendNote);
        }     
        #endregion

        #region Public Methods
        public void SendNote()
        {
            if (string.IsNullOrEmpty(TypedText))
                return;

            NoteItemViewModel item = new NoteItemViewModel { Content = TypedText };
            Items.Add(item);
            TypedText = string.Empty;
        }
        #endregion        
    }
}
