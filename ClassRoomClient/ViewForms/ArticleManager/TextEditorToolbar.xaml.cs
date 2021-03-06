﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System;

namespace ClassRoom.Views
{
    /// <summary>
    /// Interaction logic for TextEditorToolbar.xaml
    /// </summary>
    public partial class TextEditorToolbar : UserControl
    {
        public TextEditorToolbar()
        {
            InitializeComponent();
        }

        public bool IsSynchronizing { get; private set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (double i = 8; i < 48; i += 2)
            {
                fontSize.Items.Add(i);
            }
        }

        public void SynchronizeWith(TextSelection selection)
        {
            IsSynchronizing = true;

            Synchronize<double>(selection, TextBlock.FontSizeProperty, SetFontSize);
            Synchronize<FontWeight>(selection, TextBlock.FontWeightProperty, SetFontWeight);
            Synchronize<FontStyle>(selection, TextBlock.FontStyleProperty, SetFontStyle);
            Synchronize<TextDecorationCollection>(selection, TextBlock.TextDecorationsProperty, SetTextDecoration);
            Synchronize<FontFamily>(selection, TextBlock.FontFamilyProperty, SetFontFamily);

            IsSynchronizing = false;
        }

        private static void Synchronize<T>(TextSelection selection, DependencyProperty property, Action<T> methodToCall)
        {
            object value = selection.GetPropertyValue(property);
            if (value != DependencyProperty.UnsetValue) methodToCall((T)value);
        }

        private void SetFontSize(double size)
        {
            fontSize.SelectedValue = size;
        }

        private void SetFontWeight(FontWeight weight)
        {
            boldButton.IsChecked = weight == FontWeights.Bold;
        }

        private void SetFontStyle(FontStyle style)
        {
            italicButton.IsChecked = style == FontStyles.Italic;
        }

        private void SetTextDecoration(TextDecorationCollection decoration)
        {
            underlineButton.IsChecked = decoration == TextDecorations.Underline;
        }

        private void SetFontFamily(FontFamily family)
        {
            fonts.SelectedItem = family;
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}