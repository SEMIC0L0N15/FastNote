using FastNote.Core;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace FastNote
{
    class MainWindowViewModel : ViewModelBase
    { 
        private Window mWindow;
        private WindowResizer mWindowResizer;

        public bool Borderless => mWindow.WindowState == WindowState.Maximized;

        public int Border { get; set; } = 0;
        public Thickness BorderThickness => new Thickness(Borderless ? 0 : Border);

        public int ResizeBorder { get; set; } = 10;
        public Thickness ResizeBorderThickness => new Thickness(Borderless ? 0 : ResizeBorder);

        public int CaptionHeight { get; set; } = 40;
        public GridLength CaptionHeightGridLength => new GridLength(CaptionHeight);

        public int OuterMargin { get; set; } = 0;
        public Thickness OuterMarginThickness => new Thickness(OuterMargin);

        public int CornerRadiusValue { get; set; } = 0;
        public CornerRadius CornerRadius => new CornerRadius(CornerRadiusValue);

        public bool IsActive => mWindow.IsActive;

        public bool IsUpdatingData
        {
            get => ViewModelLocator.ApplicationViewModel.IsUpdatingData;
            set => ViewModelLocator.ApplicationViewModel.IsUpdatingData = value;
        }

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SystemMenuCommand { get; set; }
        public ICommand AdvancedSearchCommand { get; set; }

        public MainWindowViewModel(Window window)
        {
            mWindow = window;
            SetupEvents();
            SubscribeToApplicationViewModelPropertyChanged();
            CreateCommands();
            FixMaximizeBug();
        }

        private void SetupEvents()
        {
            mWindow.Activated += (sender, e) => RaisePropertyChanged(nameof(IsActive));
            mWindow.Deactivated += (sender, e) => RaisePropertyChanged(nameof(IsActive));
            mWindow.StateChanged += (sender, e) => OnWindowResized();
        }

        private void OnWindowResized()
        {
            RaisePropertyChanged(nameof(Borderless));
            RaisePropertyChanged(nameof(Border));
            RaisePropertyChanged(nameof(BorderThickness));
            RaisePropertyChanged(nameof(OuterMargin));
            RaisePropertyChanged(nameof(OuterMarginThickness));
            RaisePropertyChanged(nameof(ResizeBorder));
            RaisePropertyChanged(nameof(ResizeBorderThickness));
            RaisePropertyChanged(nameof(IsActive));
        }

        private void SubscribeToApplicationViewModelPropertyChanged()
        {
            ViewModelLocator.ApplicationViewModel.PropertyChanged +=
                (sender, e) => RaisePropertyChanged(e.PropertyName);
        }

        private void CreateCommands()
        {
            MinimizeCommand = new RelayCommand(Minimize);
            MaximizeCommand = new RelayCommand(Maximize);
            CloseCommand = new RelayCommand(CloseWindow);
            SystemMenuCommand = new RelayCommand(ShowSystemMenu);
            AdvancedSearchCommand = new RelayCommand(AdvancedSearchExpand);
        }

        private void Minimize()
        {
            if (mWindow.WindowState != WindowState.Maximized)
                mWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            mWindow.WindowState = WindowState.Minimized;
        }

        private void Maximize()
        {
            mWindow.WindowStyle = WindowStyle.None;
            mWindow.WindowState ^= WindowState.Maximized;
        }

        private void CloseWindow()
        {
            mWindow.Close();
        }

        private void ShowSystemMenu()
        {
            SystemCommands.ShowSystemMenu(mWindow, GetMousePosition());
        }

        private Point GetMousePosition()
        {
            return mWindowResizer.GetCursorPosition();
        }

        private void AdvancedSearchExpand()
        {
            // TODO implement AdvancedSearchExpand
        }

        private void FixMaximizeBug()
        {
            mWindowResizer = new WindowResizer(mWindow);
        } 
    }
}
