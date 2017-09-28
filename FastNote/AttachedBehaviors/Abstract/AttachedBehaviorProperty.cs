using System.Collections.Generic;
using System.Windows;

namespace FastNote
{
    public abstract class AttachedBehaviorProperty<TSelf, T> : BaseAttachedProperty<TSelf, bool>
        where TSelf: new()
        where T: DependencyObject
    {

        public static Dictionary<DependencyObject, AttachedBehavior<T>> BehaviorInstances { get; } = 
            new Dictionary<DependencyObject, AttachedBehavior<T>>();

        public sealed override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!BehaviorInstances.ContainsKey(d))
                BehaviorInstances[d] = CreateAttachedBehavior(d);

            if ((bool) e.NewValue)
                BehaviorInstances[d].OnAttached();
            else
                BehaviorInstances[d].OnDetaching();
        }

        public sealed override void OnValueUpdated(DependencyObject d, object value)
        {
            
        }

        protected abstract AttachedBehavior<T> CreateAttachedBehavior(DependencyObject d);
    }
}
