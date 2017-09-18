using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using FastNote.Core.Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    public class NoteGroupListViewModel : ViewModelBase
    {
        #region Private Members
        private INoteGroupProvider mItemProvider;
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
                Messenger.Default.Send(new SelectedNoteGroupMessage(mSelectedGroup));
            }
        }
        #endregion

        #region Constructor
        public NoteGroupListViewModel(INoteGroupProvider itemProvider)
        {
            mItemProvider = itemProvider;
            UpdateItems();
        }

        public NoteGroupListViewModel() { }
        #endregion

        #region Methods
        public void UpdateItems()
        {
            Items = new ObservableCollection<NoteGroupViewModel>(
                ConvertToViewModels(mItemProvider.GetItems()));
        }

        private IEnumerable<NoteGroupViewModel> ConvertToViewModels(IEnumerable<NoteGroup> models)
        {
            return models.Select(noteGroup => new NoteGroupViewModel(noteGroup));
        }
        #endregion
    }
}
