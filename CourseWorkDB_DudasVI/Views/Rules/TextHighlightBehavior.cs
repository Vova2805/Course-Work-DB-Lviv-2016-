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
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (TextHighlightBehavior),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof (Brush), typeof (TextHighlightBehavior),
                new PropertyMetadata(Brushes.Blue));

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof (Brush), typeof (TextHighlightBehavior),
                new PropertyMetadata(Brushes.White));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Brush Background
        {
            get { return (Brush) GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public Brush Foreground
        {
            get { return (Brush) GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        private void OnTextChanged()
        {
            if (AssociatedObject != null)
            {
                var actualText = AssociatedObject.Text;
                var queryText = Text;
                if (actualText.ToLower().Contains(queryText.ToLower()))
                {
                    var index = actualText.IndexOf(queryText, StringComparison.InvariantCultureIgnoreCase);
                    var run1 = actualText.Substring(0, index);
                    var run2 = actualText.Substring(index, queryText.Length);
                    var run3 = actualText.Substring(index + queryText.Length);

                    var newRun1 = new Run(run1);
                    var newRun2 = new Run(run2) {Foreground = Foreground, Background = Background};
                    var newRun3 = new Run(run3);

                    AssociatedObject.Inlines.Clear();
                    AssociatedObject.Inlines.Add(newRun1);
                    AssociatedObject.Inlines.Add(newRun2);
                    AssociatedObject.Inlines.Add(newRun3);
                }
            }
        }

        protected override void OnAttached()
        {
            OnTextChanged();
        }
    }
}