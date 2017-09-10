namespace FastNote.Core
{
    public static class ViewModelLocator
    {
        public static NoteBoxViewModel NoteBoxViewModel { get; } = IoC.Get<NoteBoxViewModel>();
        public static NoteGroupListViewModel NoteGroupListViewModel { get; } = IoC.Get<NoteGroupListViewModel>();
    }
}
