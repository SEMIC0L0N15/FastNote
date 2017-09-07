﻿using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;

namespace FastNote
{      
    public class SetPropertyAction : TriggerAction<FrameworkElement>
    {
        #region Dependency Properties
        
        #region PropertyName
        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public static readonly DependencyProperty PropertyNameProperty
            = DependencyProperty.Register("PropertyName", typeof(string),
            typeof(SetPropertyAction));
        #endregion
        
        #region PropertyValue
        public object PropertyValue
        {
            get => GetValue(PropertyValueProperty);
            set => SetValue(PropertyValueProperty, value);
        }

        public static readonly DependencyProperty PropertyValueProperty
            = DependencyProperty.Register("PropertyValue", typeof(object),
            typeof(SetPropertyAction));
        #endregion

        #region TargetObject
        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        public static readonly DependencyProperty TargetObjectProperty
            = DependencyProperty.Register("TargetObject", typeof(object),
            typeof(SetPropertyAction));
        #endregion

        #endregion

        #region TriggerAction<T> Methods
        protected override void Invoke(object parameter)
        {
            object target = TargetObject ?? AssociatedObject;
            PropertyInfo propertyInfo = target.GetType().GetProperty(
                PropertyName,
                BindingFlags.Instance | BindingFlags.Public
                | BindingFlags.NonPublic | BindingFlags.InvokeMethod);

            propertyInfo.SetValue(target, PropertyValue);
        } 
        #endregion
    }
}