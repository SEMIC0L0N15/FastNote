using System;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    public class SelectAllTextBehaviorProperty :
    AttachedBehaviorProperty<SelectAllTextBehaviorProperty, TextBox>
    {
        protected override AttachedBehavior<TextBox> CreateAttachedBehavior(DependencyObject d)
        {
            return new SelectAllTextBehavior((TextBox)d);
        }
    } 

    public class SelectAllTextBehavior : AttachedBehavior<TextBox>
    {
        public SelectAllTextBehavior(TextBox associatedObject)
            : base(associatedObject)
        {
        }


        public override void OnAttached()
        {
            AssociatedObject.IsVisibleChanged += OnVisibleChanged;
        }

        public override void OnDetaching()
        {
            AssociatedObject.IsVisibleChanged -= OnVisibleChanged;
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
