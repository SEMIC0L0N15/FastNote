using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FastNote.Core
{
    public class NoteBoxViewModel : ViewModelBase
    {
        #region Private Members
        private IItemsProvider<NoteItemViewModel> mItemsProvider;
        private NoteGroup mNoteGroup;
        #endregion

        #region Public Properties
        public string TypedText { get; set; }
        public ObservableCollection<NoteItemViewModel> Items { get; set; }

        public NoteGroup NoteGroup
        {
            get => mNoteGroup;
            set
            {
                mNoteGroup = value;
                UpdateItems();
            }
        }
        #endregion

        #region Public Commands
        public ICommand SendNoteCommand { get; set; }
        #endregion

        #region Constructor
        public NoteBoxViewModel(IItemsProvider<NoteItemViewModel> itemsProvider)
        {
            mItemsProvider = itemsProvider;
            SendNoteCommand = new RelayCommand(SendNote);
        }     
        #endregion

        #region Public Methods
        public void SendNote()
        {
            if (string.IsNullOrEmpty(TypedText))
                return;

            var item = new NoteItemViewModel { Content = TypedText };
            Items.Add(item);
            TypedText = string.Empty;
        }

        public void UpdateItems()
        {
            Items = new ObservableCollection<NoteItemViewModel>(mItemsProvider.GetItems(NoteGroup));
        }
        #endregion        
    }
}
