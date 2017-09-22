using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class ApplicationViewModel : ViewModelBase
    {
        public bool IsUpdatingData { get; set; }
        public bool CanHighlight { get; set; } = true;
        public bool IsDragActive { get; set; }
    }
}
