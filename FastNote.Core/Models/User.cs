using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class User : ObservableObject
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
