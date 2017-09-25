using System.Windows;

namespace FastNote
{
    public class BaseAttachedBehavior<TSelf, T> : BaseAttachedProperty<TSelf, bool>
        where TSelf: new()
        where T: DependencyObject
    {
        protected T AssociatedObject { get; private set; }

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            AssociatedObject = (T) sender;

            if ((bool) e.NewValue)
                OnAttached();
            else
                OnDetaching();
        }

        public override void OnValueUpdated(DependencyObject sender, object value) { }

        protected virtual void OnAttached() { }
        protected virtual void OnDetaching() { }
    }
}
