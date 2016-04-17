using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace CourseWorkDB_DudasVI.Views.Rules
{
    public class TextHighlightBehavior : Behavior<TextBlock>
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextHighlightBehavior), new PropertyMetadata(String.Empty));

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(TextHighlightBehavior), new PropertyMetadata(Brushes.Blue));

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }
        
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(TextHighlightBehavior), new PropertyMetadata(Brushes.White));

        private void OnTextChanged()
        {
            if (this.AssociatedObject != null)
            {
                string actualText = this.AssociatedObject.Text;
                string queryText = this.Text;
                if (actualText.ToLower().Contains(queryText.ToLower()))
                {
                    int index = actualText.IndexOf(queryText, StringComparison.InvariantCultureIgnoreCase);
                    string run1 = actualText.Substring(0, index);
                    string run2 = actualText.Substring(index, queryText.Length);
                    string run3 = actualText.Substring(index + queryText.Length);

                    var newRun1 = new Run(run1);
                    var newRun2 = new Run(run2) { Foreground = Foreground, Background = Background };
                    var newRun3 = new Run(run3);

                    this.AssociatedObject.Inlines.Clear();
                    this.AssociatedObject.Inlines.Add(newRun1);
                    this.AssociatedObject.Inlines.Add(newRun2);
                    this.AssociatedObject.Inlines.Add(newRun3);
                }
            }
        }

        protected override void OnAttached()
        {
            this.OnTextChanged();
        }
    }
}