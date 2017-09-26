using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using FastNote.Core;

namespace FastNote
{
    public abstract class EditWithArrowsBehavior<TSelf> : BaseAttachedBehavior<TSelf, TextBox>
        where TSelf : new()
    {
        protected override void OnAttached(TextBox associatedObject)
        {
            associatedObject.PreviewKeyDown += TextBox_OnPreviewKeyDown;
        }

        private void TextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox) sender;
            int lineIndex = textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex);

            switch (e.Key)
            {
                case Key.Up:
                    if (lineIndex == 0)
                    {
                        OnEditPrevious((TextBox) sender);
                        e.Handled = true;
                    }
                    break;

                case Key.Down:
                    if (lineIndex == textBox.LineCount - 1)
                    {
                        OnEditNext((TextBox) sender);
                        e.Handled = true;
                    }
                    break;

            }
        }

        protected abstract void OnEditPrevious(TextBox textBox);
        protected abstract void OnEditNext(TextBox textBox);
    }
}
