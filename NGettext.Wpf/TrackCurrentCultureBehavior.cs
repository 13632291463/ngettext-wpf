﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Interactivity;

namespace NGettext.Wpf
{
    /// <summary>
    /// Makes sure that the CultureInfo used for all binding operations inside the associated
    /// FrameworkElement follows the CurrentCulture of the CultureTracker injected to the static
    /// CultureTracker property.
    /// 确保 CultureInfo 用于关联的
    /// FrameworkElement 遵循注入到静态的 CultureTracker 的 CurrentCulture
    /// CultureTracker 属性。
    ///
    /// For instance, dates and numbers bound with a culture specific StringFormat will be formatted
    /// according to the tracked culture and even reformatted on culture changed.
    /// </summary>
    public class TrackCurrentCultureBehavior : Behavior<FrameworkElement>, IWeakCultureObserver
    {
        public static ICultureTracker CultureTracker { get; set; }

        protected override void OnAttached()
        {
            if (!DesignerProperties.GetIsInDesignMode(AssociatedObject))
            {
                if (CultureTracker is null)
                {
                    CompositionRoot.WriteMissingInitializationErrorMessage();
                    return;
                }
                CultureTracker.AddWeakCultureObserver(this);
                UpdateAssociatedObjectCulture();
            }

            base.OnAttached();
        }

        private void UpdateAssociatedObjectCulture()
        {
            if (AssociatedObject is null) return;
            AssociatedObject.Language = XmlLanguage.GetLanguage(CultureTracker.CurrentCulture.IetfLanguageTag);
        }

        public void HandleCultureChanged(ICultureTracker sender, CultureEventArgs eventArgs)
        {
            UpdateAssociatedObjectCulture();
        }
    }
}