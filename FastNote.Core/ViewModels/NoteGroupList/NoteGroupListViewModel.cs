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
        private INoteGroupProvider itemProvider;
        private NoteGroup selectedGroup;

        public ObservableCollection<NoteGroupViewModel> Items { get; set; }

        public NoteGroup SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                Messenger.Default.Send(new SelectedNoteGroupMessage(selectedGroup));
            }
        }

        public NoteGroupListViewModel(INoteGroupProvider itemProvider)
        {
            this.itemProvider = itemProvider;
            UpdateItems();
        }


        public void UpdateItems()
        {
            Items = new ObservableCollection<NoteGroupViewModel>(
                ConvertToViewModels(itemProvider.GetItems()));
        }

        private IEnumerable<NoteGroupViewModel> ConvertToViewModels(IEnumerable<NoteGroup> models)
        {
            return models.Select(noteGroup => new NoteGroupViewModel(noteGroup));
        }
    }
}
