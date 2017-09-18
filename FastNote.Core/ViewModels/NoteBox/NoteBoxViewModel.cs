using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using FastNote.Core.Database;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    [Serializable]
    public class NoteBoxViewModel : ViewModelBase
    {
        #region Private Members
        private NoteGroup mNoteGroup;
        private INoteItemProvider mItemProvider;
        private INoteItemSaver mItemSaver;
        #endregion

        #region Public Properties
        public string TypedText { get; set; }
        public ObservableCollection<NoteItemViewModel> Items { get; set; } = 
            new ObservableCollection<NoteItemViewModel>();

        public NoteGroup NoteGroup
        {
            get => mNoteGroup;
            set
            {
                if (value != null)
                {
                    mNoteGroup = value;
                    UpdateItems(); 
                }
            }
        }
        #endregion

        #region Public Commands
        [XmlIgnore]
        public ICommand PushNoteCommand { get; set; }
        #endregion

        #region Constructor
        public NoteBoxViewModel(INoteItemProvider itemProvider, INoteItemSaver itemSaver = null)
        {
            mItemProvider = itemProvider;
            mItemSaver = itemSaver;
            CreateCommands();
            SubscribeToSelectedGroupMessage();
        }

        public NoteBoxViewModel() { }

        private void CreateCommands()
        {
            PushNoteCommand = new RelayCommand(PushNote);
        }

        private void SubscribeToSelectedGroupMessage()
        {
            Messenger.Default.Register<SelectedNoteGroupMessage>(this,
                message => NoteGroup = message.SelectedGroup);
        }
        #endregion

        #region Methods

        #region PushNote
        public void PushNote()
        {
            if (NoTextTyped())
                return;
            FlushTypedText();
            SaveItems();
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

        #region SaveItems
        public void SaveItems()
        {
            mItemSaver?.SaveItems(ConvertToModels(Items).ToList(), NoteGroup.Name);
        }
        #endregion

        #region UpdateItems
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

        private static IEnumerable<NoteItem> ConvertToModels(IEnumerable<NoteItemViewModel> viewModels)
        {
            return viewModels.Select(vm => vm.NoteItem);
        }
        #endregion

        #endregion
    }
}
