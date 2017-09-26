using System;
using System.Windows;

namespace FastNote
{
    public abstract class BaseAttachedProperty<TSelf, TProperty>
        where TSelf : new()
    {
        #region Public Events
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };
        #endregion

        #region Public Properties
        public static TSelf Instance { get; private set; } = new TSelf();
        #endregion

        #region Attached Property Definitions
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value",
            typeof(TProperty),
            typeof(BaseAttachedProperty<TSelf, TProperty>),
            new UIPropertyMetadata(
                default(TProperty),
                new PropertyChangedCallback(OnValuePropertyChanged),
                new CoerceValueCallback(OnValuePropertyUpdated)
                ));
        
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (Instance as BaseAttachedProperty<TSelf, TProperty>)?.OnValueChanged(d, e);
            (Instance as BaseAttachedProperty<TSelf, TProperty>)?.ValueChanged(d, e);
        }
        
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            (Instance as BaseAttachedProperty<TSelf, TProperty>)?.OnValueUpdated(d, value);
            (Instance as BaseAttachedProperty<TSelf, TProperty>)?.ValueUpdated(d, value);

            return value;
        }
        
        public static TProperty GetValue(DependencyObject d)
        {
            return (TProperty) d.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject d, TProperty value)
        {
            d.SetValue(ValueProperty, value);
        }
        #endregion

        #region Event Handlers
        public virtual void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        public virtual void OnValueUpdated(DependencyObject d, object value) { }
        #endregion
    }
}
