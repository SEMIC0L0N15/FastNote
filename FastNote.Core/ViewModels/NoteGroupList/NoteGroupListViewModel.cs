using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    public class NoteGroupListViewModel : ViewModelBase
    {
        #region Private Members
        private IItemsProvider<NoteGroupViewModel> mItemsProvider;
        private NoteGroup mSelectedGroup;
        #endregion

        #region Public Properties
        public ObservableCollection<NoteGroupViewModel> Items { get; set; }

        public NoteGroup SelectedGroup
        {
            get => mSelectedGroup;
            set
            {
                mSelectedGroup = value;
                ViewModelLocator.NoteBoxViewModel.NoteGroup = mSelectedGroup;
            }
        }
        #endregion

        #region Constructor
        public NoteGroupListViewModel(IItemsProvider<NoteGroupViewModel> itemsProvider)
        {
            mItemsProvider = itemsProvider;
            if (mItemsProvider != null) 
                Items = new ObservableCollection<NoteGroupViewModel>(mItemsProvider.GetItems());
        }     
        #endregion
    }
}
