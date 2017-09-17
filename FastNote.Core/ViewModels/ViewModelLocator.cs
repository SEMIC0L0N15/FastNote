using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public static class ViewModelLocator
    {
        public static NoteBoxViewModel GetNoteBoxViewModel() => 
            IsInDesignMode() ? new NoteBoxDesignModel() : IoC.Get<NoteBoxViewModel>();

        public static NoteGroupListViewModel GetNoteGroupListViewModel() => 
            IsInDesignMode() ? new NoteGroupListDesignModel() : IoC.Get<NoteGroupListViewModel>();

        private static bool IsInDesignMode()
        {
            return ViewModelBase.IsInDesignModeStatic;
        }
    }
}
