using Ninject;

namespace FastNote.Core
{
    public static class IoC
    {
        #region Public Properties
        public static IKernel Kernel { get; set; } = new StandardKernel();
        #endregion

        #region Methods
        static IoC()
        {
            Kernel.Bind<INoteItemProvider>().To<DesignNoteItemProvider>();
            Kernel.Bind<INoteGroupProvider>().To<DesingNoteGroupProvider>();
        } 

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }        
        #endregion
    }
}
