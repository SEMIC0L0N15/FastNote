using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FastNote
{
    public class DoubleClickTextBlockBehavior : Behavior<TextBlock>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseDown -= OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                AssociatedObject.Tag = true;
                e.Handled = true;
            }
        }
        
    }
}
