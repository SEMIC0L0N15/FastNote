
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
            NoteGroup = new NoteGroup("dummy")
            {
                Notes = new ObservableCollection<NoteItem>()
                {
                    new NoteItem("Lords of The Fallen"),
                    new NoteItem("DeusEx: Rozłam Ludzkości"),
                    new NoteItem("Sid Meier's Civilization VI"),
                    new NoteItem("Dragon Age: Inkwizycja"),
                    new NoteItem("Mass Effect: Andromeda"),
                }
            };
        }
        #endregion
    }
}
