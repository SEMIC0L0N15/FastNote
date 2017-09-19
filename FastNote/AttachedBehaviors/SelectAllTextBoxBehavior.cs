using System;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    public class SelectAllTextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.IsVisibleChanged += OnVisibleChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.IsVisibleChanged -= OnVisibleChanged;
        }

        private void OnVisibleChanged(object o, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            FocusAndSelectAllText();
        }

        private void FocusAndSelectAllText()
        {
            AssociatedObject.Focus();
            AssociatedObject.SelectAll();
        }
    }
}
