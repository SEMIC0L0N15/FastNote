
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace FastNote.Core
{
    public class NoteBoxDesignModel : NoteBoxViewModel
    {
        #region Static Members
        public static NoteBoxDesignModel Instance = new NoteBoxDesignModel();
        #endregion

        #region Constructor
        public NoteBoxDesignModel()
            : base(null)
        {
            Items = new ObservableCollection<NoteItemViewModel>()
            {
                new NoteItemViewModel { Content = "Lords of The Fallen" },
                new NoteItemViewModel { Content = "DeusEx: Bunt Ludzkości" },
                new NoteItemViewModel { Content = "Sid Meier's Civilization VI" },
                new NoteItemViewModel { Content = "Orcs must die!" },
                new NoteItemViewModel { Content = "Mass Effect: Andromeda" },
            };

            SendNoteCommand = new RelayCommand(SendNote);
        }
        #endregion
    }
}
