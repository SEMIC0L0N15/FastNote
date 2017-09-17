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
        public class PushNoteMethod
        {
            public NoteBoxViewModel ViewModel;

            [Test]
            public void WhenTypedTextValid_AddItemWithThatText()
            {
                ConfigureViewModel();
                SetTypedText("Hello World!");

                PushNote();

                AssertChildrenCountEquals(1);
                AssertFirstItemContentEquals("Hello World!");
            }

            [Test]
            public void WhenTypedTextEmpty_DontAddItem()
            {
                ConfigureViewModel();
                SetTypedText(string.Empty);

                PushNote();

                AssertChildrenCountEquals(0);
            }

            [Test]
            public void WhenTypedTextNull_DontAddItem()
            {
                ConfigureViewModel();
                SetTypedText(null);

                PushNote();

                AssertChildrenCountEquals(0);
            }

            #region Helpers
            private void ConfigureViewModel()
            {
                var itemsProvider = A.Fake<INoteItemProvider>();
                A.CallTo(() => itemsProvider.GetItems(null)).Returns(new List<NoteItem>());

                ViewModel = new NoteBoxViewModel(itemsProvider);
            }

            private void SetTypedText(string text)
            {
                ViewModel.TypedText = text;
            }

            private void PushNote()
            {
                ViewModel.PushNote();
            }

            private void AssertChildrenCountEquals(int expectedCount)
            {
                Assert.AreEqual(expectedCount, ViewModel.Items.Count);
            }

            private void AssertFirstItemContentEquals(string expectedContent)
            {
                Assert.AreEqual(expectedContent, ViewModel.Items[0].Content);
            }
            #endregion
        }


    }    
}
