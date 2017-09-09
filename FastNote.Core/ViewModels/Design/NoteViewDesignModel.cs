using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNote.Core
{
    public class NoteBoxDesignModel : NoteBoxViewModel
    {
        public static NoteBoxDesignModel Instance = 
            new NoteBoxDesignModel();

        public NoteBoxDesignModel()
        {
            Items = new ObservableCollection<NoteItemViewModel>()
            {
                new NoteItemViewModel { Content = "Lords of The Fallen" },
                new NoteItemViewModel { Content = "DeusEx: Bunt Ludzkości" },
                new NoteItemViewModel { Content = "Sid Meier's Civilization VI" },
                new NoteItemViewModel { Content = "Orcs must die!" },
                new NoteItemViewModel { Content = "Mass Effect: Andromeda" },
            };
        }
    }
}
