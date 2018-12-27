using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FastNote.Core;

namespace FastNote
{
    public class ItemEditorEditWithArrowsBehaviorProperty :
    AttachedBehaviorProperty<ItemEditorEditWithArrowsBehaviorProperty, TextBox>
    {
        protected override AttachedBehavior<TextBox> CreateAttachedBehavior(DependencyObject d)
        {
            return new ItemEditorEditWithArrowsBehavior((TextBox)d);
        }
    } 

    public class ItemEditorEditWithArrowsBehavior: EditWithArrowsBehavior
    {
        public ItemEditorEditWithArrowsBehavior(TextBox associatedObject) :
            base(associatedObject)
        {

        } 

        protected override void OnEditPrevious(TextBox textBox)
        {
            var currentItem = (NoteItemViewModel) textBox.DataContext;
            var items = ViewModelLocator.NoteBoxViewModel.Items;

            int index = items.IndexOf(currentItem);
            if (index == 0) return;

            NoteItemViewModel previousItem = items[index - 1];

            currentItem.SubmitEdit();
            previousItem.StartEditing();
        }

        protected override void OnEditNext(TextBox textBox)
        {
            var currentItem = (NoteItemViewModel) textBox.DataContext;
            var items = ViewModelLocator.NoteBoxViewModel.Items;

            int index = items.IndexOf(currentItem);
            if (index == items.Count - 1) return;

            NoteItemViewModel nextItem = items[index + 1];

            currentItem.SubmitEdit();
            nextItem.StartEditing();
        }
    }
}
