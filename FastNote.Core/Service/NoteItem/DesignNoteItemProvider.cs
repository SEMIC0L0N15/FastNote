using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;

namespace FastNote.Core
{
    public class DesignNoteItemProvider : IItemsProvider<NoteItemViewModel>
    {
        public IEnumerable<NoteItemViewModel> GetItems()
        {
            IEnumerable<NoteItemViewModel> items = new List<NoteItemViewModel>()
            {
                new NoteItemViewModel {Content = "Lords of The Fallen"},
                new NoteItemViewModel {Content = "DeusEx: Bunt Ludzkości"},
                new NoteItemViewModel {Content = "Sid Meier's Civilization VI"},
                new NoteItemViewModel {Content = "Orcs must die!"},
                new NoteItemViewModel {Content = "Mass Effect: Andromeda"},
            };

            return items;
        }

        public IEnumerable<NoteItemViewModel> GetItems(object parameter)
        {
            var selectedGroup = parameter as NoteGroup;

            IEnumerable<NoteItemViewModel> items = new List<NoteItemViewModel>()
            {
                new NoteItemViewModel {Content = selectedGroup?.Name},
            };

            return items;
        }
    }
}