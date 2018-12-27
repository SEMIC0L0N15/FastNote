using System;
using System.Runtime.Remoting.Channels;
using System.Windows.Input;
using FastNote.Core.Message;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    public class NoteItemViewModel : ViewModelBase
    {
        public NoteItem NoteItem { get; set; }
        public bool IsSelected { get; set; }
        public bool IsBeingEdited { get; set; }
        public string TypedText { get; set; }

        public string Content
        {
            get => NoteItem.Content;
            set
            {
                NoteItem.Content = value;
                Messenger.Default.Send<NoteContentChangedMessage>(new NoteContentChangedMessage(NoteItem));
            }
        }

        public DateTime CreationDate
        {
            get => NoteItem.CreationDate;
            set => NoteItem.CreationDate = value;
        }

        public ICommand EditNoteCommand { get; set; }
        public ICommand SubmitEditCommand { get; set; }
        public ICommand DiscardEditCommand { get; set; }

        public NoteItemViewModel(NoteItem noteItem)
        {
            NoteItem = noteItem;
            TypedText = Content;
            SubscribeToModelPropertyChanged();
            CreateCommands();
        }

        private void SubscribeToModelPropertyChanged()
        {
            NoteItem.PropertyChanged += (sender, e) => RaisePropertyChanged(e.PropertyName);
        }

        private void CreateCommands()
        {
            EditNoteCommand = new RelayCommand(StartEditing);
            SubmitEditCommand = new RelayCommand(SubmitEdit);
            DiscardEditCommand = new RelayCommand(DiscardEdit);
        }

        public void StartEditing()
        {
            IsBeingEdited = true;
        }

        public void SubmitEdit()
        {
            Content = TypedText;
            IsBeingEdited = false;
        }

        public void DiscardEdit()
        {
            TypedText = Content;
            IsBeingEdited = false;
        }
    }
}