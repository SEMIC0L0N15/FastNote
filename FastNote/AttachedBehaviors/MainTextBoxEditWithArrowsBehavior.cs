using System.Linq;
using System.Windows.Controls;
using FastNote.Core;

namespace FastNote
{
    public class MainTextBoxEditWithArrowsBehavior : EditWithArrowsBehavior<MainTextBoxEditWithArrowsBehavior>
    {
        protected override void OnEditPrevious(TextBox textBox)
        {
            ViewModelLocator.NoteBoxViewModel.Items.Last().StartEditing();
        }

        protected override void OnEditNext(TextBox textBox)
        {
            
        }
    }
}
