using FastNote.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNote.Test
{
    [TestFixture]
    public class NoteBoxViewModelTest
    {
        public class SendNoteMethod
        {
            [Test]
            public void WhenTypedTextValid_AddItemWithThatText()
            {
                NoteBoxViewModel vm = new NoteBoxViewModel();
                string text = "Hello world!";
                vm.TypedText = text;

                vm.SendNote();

                Assert.AreEqual(1, vm.Items.Count);
                Assert.AreEqual(text, vm.Items[0].Content);
            }

            [Test]
            public void WhenTypedTextEmpty_NotAddItem()
            {
                NoteBoxViewModel vm = new NoteBoxViewModel();
                vm.TypedText = string.Empty;

                vm.SendNote();

                Assert.AreEqual(0, vm.Items.Count);
            }

            [Test]
            public void WhenTypedTextNull_NotAddItem()
            {
                NoteBoxViewModel vm = new NoteBoxViewModel();
                vm.TypedText = null;

                vm.SendNote();

                Assert.AreEqual(0, vm.Items.Count);
            }
        }
        
    }    
}
