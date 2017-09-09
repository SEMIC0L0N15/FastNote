using FakeItEasy;
using FastNote.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FastNote.Test
{
    [TestFixture]
    public class NoteGroupListViewModelTest
    {
        [Test]
        public void FunWithFakeItEasy()
        {
            IItemsProvider<int> itemsProvider = A.Fake<IItemsProvider<int>>();

            A.CallTo(() => itemsProvider.GetItems()).Returns<IEnumerable<int>>(new List<int>() { 5, 4, 3, 2, 1 });

            foreach (var a in itemsProvider.GetItems())
            {
                
            }
        }
    }
}
