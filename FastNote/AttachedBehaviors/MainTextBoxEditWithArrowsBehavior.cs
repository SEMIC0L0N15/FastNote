using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FastNote.Core;

namespace FastNote
{
    #region Property
    public class MainTextBoxEditWithArrowsBehaviorProperty :
    AttachedBehaviorProperty<MainTextBoxEditWithArrowsBehaviorProperty, TextBox>
    {
        protected override AttachedBehavior<TextBox> CreateAttachedBehavior(DependencyObject d)
        {
            return new MainTextBoxEditWithArrowsBehavior((TextBox)d);
        }
    } 
    #endregion

    public class MainTextBoxEditWithArrowsBehavior : EditWithArrowsBehavior
    {
        #region Contructor
        public MainTextBoxEditWithArrowsBehavior(TextBox associatedObject)
            : base(associatedObject)
        {
        } 
        #endregion

        protected override void OnEditPrevious(TextBox textBox)
        {
            ViewModelLocator.NoteBoxViewModel.Items.Last().StartEditing();
        }

        protected override void OnEditNext(TextBox textBox)
        {
            
        }
    }
}
