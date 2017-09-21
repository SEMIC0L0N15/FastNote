using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FastNote
{
    public static class MarginSetter
    {
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(MarginSetter), new UIPropertyMetadata(new Thickness(), OnMarginChanged));

        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness) obj.GetValue(MarginProperty);
        }
    
        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }              
    
        public static void OnMarginChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if( sender is Panel panel )
                panel.Loaded += new RoutedEventHandler(panel_Loaded);    
        }
    
        static void panel_Loaded(object sender, RoutedEventArgs e)
        {            
            var panel = sender as Panel;
    
            foreach (var child in panel.Children)
            {         
                if (child is FrameworkElement element)
                    element.Margin = MarginSetter.GetMargin(panel);
            }
        }    
    
    }
}
     