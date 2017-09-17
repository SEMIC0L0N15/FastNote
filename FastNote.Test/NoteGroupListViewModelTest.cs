using System.Collections.Generic;
using FakeItEasy;
using FastNote.Core;
using NUnit.Framework;

namespace FastNote.Test
{
    [TestFixture]
    public class NoteGroupListViewModelTest
    {
        public class SelectingGroupsFeature
        {
            #region Public Fields
            public List<NoteGroup> NoteGroups;
            public List<NoteItem> FirstGroupItems;
            public List<NoteItem> SecondGroupItems;

            public NoteGroupListViewModel NoteGroupListViewModel;
            public NoteBoxViewModel NoteBoxViewModel;

            public INoteGroupProvider NoteGroupProvider;
            public INoteItemProvider NoteItemProvider; 
            #endregion

            [Test]
            public void WhenSelectedFirstGroup_FirstGroupItemsAppeasInNoteBox()
            {
                CreateGroupsAndItems();
                ConfigureViewModels();

                SelectNoteGroup(NoteGroups[0]);

                AssertCallToGetItemsMustHaveHappenedFor(NoteGroups[0]);
                AssertNoteBoxFirstItemContentEquals("firstGroupItem1");
            }

            [Test]
            public void WhenSelectedSecondGroup_SecondGroupItemsAppeasInNoteBox()
            {
                CreateGroupsAndItems();
                ConfigureViewModels();

                SelectNoteGroup(NoteGroups[1]);

                AssertCallToGetItemsMustHaveHappenedFor(NoteGroups[1]);
                AssertNoteBoxFirstItemContentEquals("secondGroupItem1");
            }

            [Test]
            public void WhenSelectedGroupIsNull_CallToGetItemsMustNotHaveHappened()
            {
                CreateGroupsAndItems();
                ConfigureViewModels();

                // we have to preselect some NoteGroup since FodyWeaver won't 
                // invoke setter if the property haven't actually changed
                SelectNoteGroup(NoteGroups[0]); 
                SelectNoteGroup(null);

                AssertCallToGetItemsMustNotHaveHappenedFor(null);
            }

            #region CreateGroupsAndModels
            private void CreateGroupsAndItems()
            {
                CreateNoteGroups();
                CreateNoteItems();
            }

            private void CreateNoteGroups()
            {
                NoteGroups = new List<NoteGroup>
                {
                    new NoteGroup("firstGroup"),
                    new NoteGroup("secondGroup"),
                };
            }

            private void CreateNoteItems()
            {
                FirstGroupItems = new List<NoteItem>
                {
                    new NoteItem("firstGroupItem1"),
                };

                SecondGroupItems = new List<NoteItem>
                {
                    new NoteItem("secondGroupItem1")
                };
            }
            
            #endregion

            #region ConfigureViewModels
            private void ConfigureViewModels()
            {
                ConfigureNoteGroupViewModel();
                ConfigureNoteBoxViewModel();
            }

            private void ConfigureNoteGroupViewModel()
            {
                NoteGroupProvider = A.Fake<INoteGroupProvider>();
                A.CallTo(() => NoteGroupProvider.GetItems()).Returns(NoteGroups);
                NoteGroupListViewModel = new NoteGroupListViewModel(NoteGroupProvider);
            }

            private void ConfigureNoteBoxViewModel()
            {
                NoteItemProvider = A.Fake<INoteItemProvider>();
                A.CallTo(() => NoteItemProvider.GetItems(NoteGroups[0])).Returns(FirstGroupItems);
                A.CallTo(() => NoteItemProvider.GetItems(NoteGroups[1])).Returns(SecondGroupItems);
                A.CallTo(() => NoteItemProvider.GetItems(null)).Returns(null);
                NoteBoxViewModel = new NoteBoxViewModel(NoteItemProvider);
            }
            #endregion

            #region SelectNoteGroup
            private void SelectNoteGroup(NoteGroup noteGroup)
            {
                NoteGroupListViewModel.SelectedGroup = noteGroup;
            }
            #endregion

            #region Assertions
            private void AssertCallToGetItemsMustHaveHappenedFor(NoteGroup noteGroup)
            {
                A.CallTo(() => NoteItemProvider.GetItems(noteGroup)).MustHaveHappened();
            }

            private void AssertCallToGetItemsMustNotHaveHappenedFor(NoteGroup noteGroup)
            {
                A.CallTo(() => NoteItemProvider.GetItems(noteGroup)).MustNotHaveHappened();
            }

            private void AssertNoteBoxFirstItemContentEquals(string content)
            {
                Assert.True(NoteBoxViewModel.Items[0].NoteItem.Content == content);
            }
            #endregion
        }
    }
    
}
