using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        #endregion

        #region Public Properties
        public string TypedText { get; set; }
        public List<NoteItemViewModel> Items { get; set; }

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
        public NoteBoxViewModel()
        {
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
        public void PushNote()
        {
            if (NoTextTyped())
                return;
            FlushTypedText();
            UpdateItems();
        }

        private bool NoTextTyped()
        {
            return string.IsNullOrEmpty(TypedText);
        }

        private void FlushTypedText()
        {
            NoteGroup.AddNote(new NoteItem(TypedText));
            TypedText = string.Empty;
        }

        public void UpdateItems()
        {
            Items = new List<NoteItemViewModel>(ConvertToViewModels(NoteGroup.Notes));
        }

        private static IEnumerable<NoteItemViewModel> ConvertToViewModels(IEnumerable<NoteItem> models)
        {
            return models.Select((noteItem) => new NoteItemViewModel(noteItem));
        }
        #endregion        
    }
}
