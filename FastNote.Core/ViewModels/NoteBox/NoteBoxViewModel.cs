using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using FastNote.Core.Database;
using FastNote.Core.Message;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    [Serializable]
    public class NoteBoxViewModel : ViewModelBase
    {
        #region Private and Protected Members
        protected NoteGroup noteGroup = new NoteGroup();
        #endregion

        #region Public Properties
        public string TypedText { get; set; }
        public List<NoteItemViewModel> Items { get; set; } = 
            new List<NoteItemViewModel>();

        public virtual NoteGroup NoteGroup
        {
            get => noteGroup;
            set
            {
                if (value != null)
                {
                    noteGroup.PropertyChanged -= OnNoteGroupPropertyChanged;

                    noteGroup = value;

                    noteGroup.UpdateNotes();
                    noteGroup.PropertyChanged += OnNoteGroupPropertyChanged;
                    RefreshItems(); 
                }
            }
        }
        #endregion

        #region Public Commands
        public ICommand PushNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }
        #endregion

        #region Constructor
        public NoteBoxViewModel()
        {
            CreateCommands();
            RegisterToSelectedGroupMessage();
            RegisterToNoteContentChangedMessage();
        }

        private void CreateCommands()
        {
            PushNoteCommand = new RelayCommand(PushNote);
            DeleteNoteCommand = new RelayCommand(DeleteSelectedNotes);
        }

        private void RegisterToSelectedGroupMessage()
        {
            Messenger.Default.Register<SelectedNoteGroupMessage>(this,
                message => NoteGroup = message.SelectedGroup);
        }

        private void RegisterToNoteContentChangedMessage()
        {
            Messenger.Default.Register<NoteContentChangedMessage>(this, 
                message => SaveItems());
        }
        #endregion

        #region Methods

        #region Adding Notes
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
            AddNote(new NoteItem(TypedText));
            TypedText = string.Empty;
        }

        public void AddNote(NoteItem noteItem)
        {
            NoteGroup.AddNote(noteItem);
        }
        #endregion

        #region Deleting Notes
        public void DeleteSelectedNotes()
        {
            var tempItems = new ObservableCollection<NoteItemViewModel>(Items);

            foreach (var item in tempItems)
            {
                if (item.IsSelected)
                    DeleteNote(item);
            }
        }

        public void DeleteNote(NoteItemViewModel note)
        {
            NoteGroup.DeleteNote(note.NoteItem);
        }
        #endregion

        #region Saving Notes
        public void SaveItems()
        {
            NoteGroup.SaveNotes();
        }
        #endregion

        #region Refreshing Notes
        public void RefreshItems()
        {
            // using private member instead of property due to stack overflow problem
            Items = ConvertToViewModels(noteGroup.GetNotes());
        }
        #endregion

        #endregion

        #region Helpers
        private static List<NoteItemViewModel> ConvertToViewModels(IEnumerable<NoteItem> models)
        {
            return models.Select((noteItem) => new NoteItemViewModel(noteItem)).ToList();
        }

        private static List<NoteItem> ConvertToModels(IEnumerable<NoteItemViewModel> viewModels)
        {
            return viewModels.Select(vm => vm.NoteItem).ToList();
        }
        #endregion

        #region Event Handlers
        public void OnNoteGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshItems();
        }
        #endregion
    }
}
