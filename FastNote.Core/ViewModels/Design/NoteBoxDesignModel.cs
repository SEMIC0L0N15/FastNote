
using System.Collections.Generic;
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
        {
            Items = new List<NoteItemViewModel>()
            {
                new NoteItemViewModel(new NoteItem("Lords of The Fallen")),
                new NoteItemViewModel(new NoteItem("DeusEx: Bunt Ludzkości")),
                new NoteItemViewModel(new NoteItem("Sid Meier's Civilization VI")),
                new NoteItemViewModel(new NoteItem("Orcs must die!")),
                new NoteItemViewModel(new NoteItem("Mass Effect: Andromeda")),
            };
        }
        #endregion
    }
}
