using System;
using System.Windows.Input;

namespace FastNote
{
    class RelayCommand : ICommand
    {
        #region Private Members
        private Action mAction;
        #endregion

        #region Public Events
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion

        #region ICommand Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
        #endregion
    }
}
