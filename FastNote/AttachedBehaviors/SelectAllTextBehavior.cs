using System;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    #region Property
    public class SelectAllTextBehaviorProperty :
    AttachedBehaviorProperty<SelectAllTextBehaviorProperty, TextBox>
    {
        protected override AttachedBehavior<TextBox> CreateAttachedBehavior(DependencyObject d)
        {
            return new SelectAllTextBehavior((TextBox)d);
        }
    } 
    #endregion

    public class SelectAllTextBehavior : AttachedBehavior<TextBox>
    {
        #region Constructor
        public SelectAllTextBehavior(TextBox associatedObject)
            : base(associatedObject)
        {
        }

        #endregion

        #region Attach/Detach
        public override void OnAttached()
        {
            AssociatedObject.IsVisibleChanged += OnVisibleChanged;
        }

        public override void OnDetaching()
        {
            AssociatedObject.IsVisibleChanged -= OnVisibleChanged;
        } 
        #endregion

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
