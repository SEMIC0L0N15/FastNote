using FastNote.Core;
using System.Windows;
using System.Windows.Input;

namespace FastNote
{
    class MainWindowViewModel : BaseViewModel
    { 
        #region Private Mambers
        private Window mWindow;
        #endregion

        #region Public Properties
        public bool Borderless => mWindow.WindowState == WindowState.Maximized;

        public int Border { get; set; } = 0;
        public Thickness BorderThickness => new Thickness(Borderless ? 0 : Border);

        public int ResizeBorder { get; set; } = 5;
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder);

        public int CaptionHeight { get; set; } = 40;
        public GridLength CaptionHeightGridLength => new GridLength(CaptionHeight);

        public int OuterMargin { get; set; } = 0;
        public Thickness OuterMarginThickness => new Thickness(OuterMargin);

        public int CornerRadiusSize { get; set; } = 0;
        public CornerRadius CornerRadius => new CornerRadius(CornerRadiusSize);

        public bool IsActive => mWindow.IsActive;
        #endregion

        #region Public Commands
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SystemMenuCommand { get; set; }
        #endregion

        #region Constructor and it's helpers
        public MainWindowViewModel(Window window)
        {
            mWindow = window;
            SetupEvents();
            CreateCommands();
            FixMaximizeBug();
        }

        #region Contructor Helpers
        private void SetupEvents()
        {
            mWindow.Activated += (sender, e) => OnPropertyChanged(nameof(IsActive));
            mWindow.Deactivated += (sender, e) => OnPropertyChanged(nameof(IsActive));
            mWindow.StateChanged += (sender, e) => OnWindowResized();
        }

        private void OnWindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(Border));
            OnPropertyChanged(nameof(BorderThickness));
            OnPropertyChanged(nameof(OuterMargin));
            OnPropertyChanged(nameof(OuterMarginThickness));
            OnPropertyChanged(nameof(ResizeBorder));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(IsActive));
        }

        private void CreateCommands()
        {
            CreateMinimizeCommand();
            CreateMaximizeCommand();
            CreateCloseCommand();
            CreateSystemMenuCommand();
        }

        private void CreateMinimizeCommand()
        {
            MinimizeCommand = new RelayCommand(() =>
            {
                if (mWindow.WindowState != WindowState.Maximized)
                    mWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                mWindow.WindowState = WindowState.Minimized;
            });
        }

        private void CreateMaximizeCommand()
        {
            MaximizeCommand = new RelayCommand(() =>
            {
                mWindow.WindowStyle = WindowStyle.None;
                mWindow.WindowState ^= WindowState.Maximized;
            });
        }

        private void CreateCloseCommand()
        {
            CloseCommand = new RelayCommand(() => mWindow.Close());
        }

        private void CreateSystemMenuCommand()
        {
            SystemMenuCommand =
                new RelayCommand(
                    () => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));
        }

        private Point GetMousePosition()
        {
            Point position = Mouse.GetPosition(mWindow);
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        private void FixMaximizeBug()
        {
            WindowResizer resizer = new WindowResizer(mWindow);
        } 
        #endregion

        #endregion
    }
}
