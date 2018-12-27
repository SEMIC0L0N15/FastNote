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
        protected NoteGroup noteGroup = new NoteGroup();

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

        public ICommand PushNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

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

        public void DeleteSelectedNotes()
        {
            var tempItems = new ObservableCollection<NoteItemViewModel>(Items);

            foreach (var item in tempItems)
            {
                if (item.IsSelected)
                    DeleteNote(item.NoteItem);
            }
        }

        public void DeleteNote(NoteItem noteItem)
        {
            NoteGroup.DeleteNote(noteItem);
        }

        public void SaveItems()
        {
            NoteGroup.SaveNotes();
        }

        public void RefreshItems()
        {
            // using private member instead of property due to stack overflow problem
            Items = ConvertToViewModels(noteGroup.GetNotes());
            RaisePropertyChanged(nameof(Items));
        }

        private static List<NoteItemViewModel> ConvertToViewModels(IEnumerable<NoteItem> models)
        {
            return models.Select((noteItem) => new NoteItemViewModel(noteItem)).ToList();
        }

        private static List<NoteItem> ConvertToModels(IEnumerable<NoteItemViewModel> viewModels)
        {
            return viewModels.Select(vm => vm.NoteItem).ToList();
        }

        public void OnNoteGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshItems();
        }
    }
}
