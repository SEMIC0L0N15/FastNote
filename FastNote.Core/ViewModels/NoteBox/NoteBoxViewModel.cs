using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    public class NoteBoxViewModel : ViewModelBase
    {
        #region Private Members
        private NoteGroup mNoteGroup = new NoteGroup("dummy");
        private INoteItemProvider mItemProvider;
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
        public ICommand PushNoteCommand { get; set; }
        #endregion

        #region Constructor
        public NoteBoxViewModel(INoteItemProvider itemProvider)
        {
            mItemProvider = itemProvider;
            CreateCommands();
            SubscribeToSelectedGroupMessage();
            UpdateItems();
        }

        private void CreateCommands()
        {
            PushNoteCommand = new RelayCommand(PushNote);
        }

        private void SubscribeToSelectedGroupMessage()
        {
            Messenger.Default.Register<SelectedNoteGroupMessage>(this,
                (message) => NoteGroup = message.SelectedGroup);
        }
        #endregion

        #region Methods

        #region PushNote Method
        public void PushNote()
        {
            if (NoTextTyped())
                return;
            FlushTypedText();
            Task.Run(async () => await SaveItemsAsync());
        }

        private bool NoTextTyped()
        {
            return string.IsNullOrEmpty(TypedText);
        }

        private void FlushTypedText()
        {
            Items.Add(new NoteItemViewModel(new NoteItem(TypedText)));
            TypedText = string.Empty;
        }
        #endregion

        #region SaveItems Method
        public async Task SaveItemsAsync()
        {
            SaveItemsLocally();
            await SaveItemsToDatabaseAsync();
        }

        private void SaveItemsLocally()
        {
            // TODO implement SaveItemsLocally
        }

        private async Task SaveItemsToDatabaseAsync()
        {
            // TODO implement SaveItemsToDatabase
        }
        #endregion

        #region UpdateItems Method
        public void UpdateItems()
        {
            var providedItems = mItemProvider.GetItems(NoteGroup);

            Items = new ObservableCollection<NoteItemViewModel>(
                ConvertToViewModels(providedItems));
        }

        private static IEnumerable<NoteItemViewModel> ConvertToViewModels(IEnumerable<NoteItem> models)
        {
            return models.Select((noteItem) => new NoteItemViewModel(noteItem));
        } 
        #endregion

        #endregion
    }
}
