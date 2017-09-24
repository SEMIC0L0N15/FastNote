using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class ApplicationViewModel : ViewModelBase
    {
        public bool IsUpdatingData { get; set; }
        public bool IsDragActive => DraggingObject != null;
        public object DraggingObject { get; set; }
    }
}
