using FastNote.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;

namespace FastNote.Test
{
    [TestFixture]
    public class NoteBoxViewModelTest
    {
        public class SendNoteMethod
        {
            private NoteBoxViewModel viewModel;

            [Test]
            public void WhenTypedTextValid_AddItemWithThatText()
            {
                SetupViewModel();
                SetTypedText("Hello World!");

                SendNote();

                AssertChildrenCountEquals(1);
                AssertFirstItemContentEquals("Hello World!");
            }

            [Test]
            public void WhenTypedTextEmpty_NotAddItem()
            {
                SetupViewModel();
                SetTypedText(string.Empty);

                SendNote();

                AssertChildrenCountEquals(0);
            }

            [Test]
            public void WhenTypedTextNull_NotAddItem()
            {
                SetupViewModel();
                SetTypedText(null);

                SendNote();

                AssertChildrenCountEquals(0);
            }

            #region Helpers
            private void SetupViewModel()
            {
                var itemsProvider = A.Fake<IItemsProvider<NoteItemViewModel>>();
                A.CallTo(() => itemsProvider.GetItems()).Returns(new List<NoteItemViewModel>());
                viewModel = new NoteBoxViewModel(itemsProvider);
            }

            private void SetTypedText(string text)
            {
                viewModel.TypedText = text;
            }

            private void SendNote()
            {
                viewModel.SendNote();
            }

            private void AssertChildrenCountEquals(int expectedCount)
            {
                Assert.AreEqual(expectedCount, viewModel.Items.Count);
            }

            private void AssertFirstItemContentEquals(string expectedContent)
            {
                Assert.AreEqual(expectedContent, viewModel.Items[0].Content);
            }
            #endregion
        }


    }    
}
