using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FastNote.Core
{
    public class NoteGroupListViewModel : BaseViewModel
    {
        #region Private Members
        private IItemsProvider<NoteGroupViewModel> mItemsProvider;
        #endregion

        #region Public Properties
        public ObservableCollection<NoteGroupViewModel> Items { get; set; }
        #endregion

        #region Constructor
        public NoteGroupListViewModel(IItemsProvider<NoteGroupViewModel> itemsProvider)
        {
            mItemsProvider = itemsProvider;
            Items = new ObservableCollection<NoteGroupViewModel>(mItemsProvider.GetItems());
        }     
        #endregion
    }
}
