
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace FastNote.Core
{
    public sealed class NoteBoxDesignModel : NoteBoxViewModel
    {
        #region Static Members
        public static NoteBoxDesignModel Instance { get; set; } = new NoteBoxDesignModel();
        #endregion

        #region Constructor
        public NoteBoxDesignModel()
        {
            NoteGroup = new NoteGroup();
            NoteGroup.AddNote(new NoteItem("Lords of The Fallen"));
            NoteGroup.AddNote(new NoteItem("DeusEx: Rozłam Ludzkości"));
            NoteGroup.AddNote(new NoteItem("Sid Meier's Civilization VI"));
            NoteGroup.AddNote(new NoteItem("Dragon Age: Inkwizycja"));
            NoteGroup.AddNote(new NoteItem("Mass Effect: Andromeda"));
            RefreshItems();
        }
        #endregion

        public override NoteGroup NoteGroup
        {
            get => noteGroup;
            set => noteGroup = value;
        }
    }
}
