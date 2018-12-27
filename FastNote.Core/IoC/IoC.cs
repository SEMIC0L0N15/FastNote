using Ninject;

namespace FastNote.Core
{
    public static class IoC
    {
        public static IKernel Kernel { get; set; } = new StandardKernel();

        static IoC()
        {
            Kernel.Bind<INoteItemProvider>().To<LocalNoteItemProvider>();
            Kernel.Bind<INoteGroupProvider>().To<DesignNoteGroupProvider>();
            Kernel.Bind<INoteItemSaver>().To<LocalNoteItemSaver>();

            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
            Kernel.Bind<NoteBoxViewModel>().ToConstant(new NoteBoxViewModel());
            Kernel.Bind<NoteGroupListViewModel>().ToConstant(new NoteGroupListViewModel(new DesignNoteGroupProvider()));


        } 

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }        
    }
}
