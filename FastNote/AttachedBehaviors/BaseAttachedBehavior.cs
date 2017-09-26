using System.Windows;

namespace FastNote
{
    public class BaseAttachedBehavior<TSelf, T> : BaseAttachedProperty<TSelf, bool>
        where TSelf: new()
        where T: DependencyObject
    {

        public sealed override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
                OnAttached((T) d);
            else
                OnDetaching((T) d);
        }

        public sealed override void OnValueUpdated(DependencyObject d, object value) { }

        protected virtual void OnAttached(T associatedObject) { }
        protected virtual void OnDetaching(T associatedObjecy) { }
    }
}
