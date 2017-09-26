using System;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    public class SelectAllTextBehavior : BaseAttachedBehavior<SelectAllTextBehavior, TextBox>
    {
        protected override void OnAttached(TextBox associatedObject)
        {
            associatedObject.IsVisibleChanged += OnVisibleChanged;
        }

        protected override void OnDetaching(TextBox associatedObject)
        {
            associatedObject.IsVisibleChanged -= OnVisibleChanged;
        }

        private void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FocusAndSelectAllText((TextBox) sender);
        }

        private void FocusAndSelectAllText(TextBox associatedObject)
        {
            associatedObject.Focus();
            associatedObject.SelectAll();
        }
    }
}
