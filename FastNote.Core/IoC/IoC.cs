using Ninject;

namespace FastNote.Core
{
    public static class IoC
    {
        #region Public Properties
        public static IKernel Kernel { get; set; } = new StandardKernel();
        #endregion

        #region Public Methods
        public static void Setup()
        {
            Kernel.Bind<IItemsProvider<NoteGroupViewModel>>().To<DesignNoteGroupProvider>();
        } 

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }        
        #endregion
    }
}
