﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    public class NoteGroupListViewModel : ViewModelBase
    {
        #region Private and Protected Members
        private INoteGroupProvider mItemsProvider;
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
        public NoteGroupListViewModel(INoteGroupProvider itemsProvider)
        {
            mItemsProvider = itemsProvider;
            UpdateItems();
        }
        #endregion

        #region Methods
        public void UpdateItems()
        {
            Items = new ObservableCollection<NoteGroupViewModel>(
                ConvertToViewModels(mItemsProvider.GetItems(null)));
        }

        private IEnumerable<NoteGroupViewModel> ConvertToViewModels(IEnumerable<NoteGroup> models)
        {
            return models.Select((noteGroup) => new NoteGroupViewModel(noteGroup));
        }
        #endregion
    }
}