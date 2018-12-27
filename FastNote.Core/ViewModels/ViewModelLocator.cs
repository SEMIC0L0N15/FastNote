using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public static class ViewModelLocator
    {
        public static NoteBoxViewModel NoteBoxViewModel =>
            IsInDesignMode() ? new NoteBoxDesignModel() : IoC.Get<NoteBoxViewModel>();

        public static NoteGroupListViewModel NoteGroupListViewModel =>
            IsInDesignMode() ? new NoteGroupListDesignModel() : IoC.Get<NoteGroupListViewModel>();

        public static ApplicationViewModel ApplicationViewModel => 
            IoC.Get<ApplicationViewModel>();

        private static bool IsInDesignMode()
        {
            return ViewModelBase.IsInDesignModeStatic;
        } 
    }
}
