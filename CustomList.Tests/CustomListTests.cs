using CustomList.Library;
using System.Collections.Generic;
using Xunit;


namespace CustomList.Tests
{
    public class CustomListTests
    {
        #region Add

        [Fact]
        public void AddItemTests()
        {
            CustomList<string> list = new CustomList<string>() { "A", "B", "C" };

            list.Add("D");

            Assert.Equal("ABCD", string.Join("", list));
        }

        [Fact]
        public void AddNodeTests()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };

            CustomNode<int> node = new CustomNode<int>(4);
            list.Add(node);

            Assert.Equal("1234", string.Join("", list));
        }

        #endregion

        #region Contains

        [Fact]
        public void ContainsInstemTests()
        {
            test(new CustomList<string>() { "O", "A", "E" }, "A", true);
            test(new CustomList<string>() { "C", "S", "S" }, "G", false);

            void test(CustomList<string> list, string element, bool result)
            {
                Assert.Equal(list.Contains(element), result);
            }
        }

        [Fact]
        public void ContainsNodeTests()
        {
            var list = new CustomList<int>() { 1, 5, 8 };
            test(list, list.GetNodeByIndex(2), true);
            test(new CustomList<int>() { 9, 3, 4 }, new CustomNode<int>(0), false);

            void test(CustomList<int> list, CustomNode<int> node, bool result)
            {
                Assert.Equal(list.Contains(node), result);
            }
        }

        #endregion

        #region Remove

        [Fact]
        public void RemoveItemTests()
        {
            CustomList<string> list = new CustomList<string>() { "A", "B", "C" };

            list.Remove("C");

            Assert.Equal("AB", string.Join("", list));
        }

        [Fact]
        public void RemoveNodeTests()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };

            CustomNode<int> node = list.GetNodeByIndex(list.Count - 1);
            list.Remove(node);

            Assert.Equal("12", string.Join("", list));
        }

        #endregion

        #region Other
        [Fact]
        public void GetNodeByIndexTest()
        {
            CustomList<string> list = new CustomList<string>() { "A", "B", "C" };

            CustomNode<string> node = new CustomNode<string>("D");
            list.Add(node);

            var nodeByIndex = list.GetNodeByIndex(list.Count - 1);

            Assert.Equal(node, nodeByIndex);
        }

        [Fact]
        public void FindIndexTest()
        {
            CustomList<int> list = new CustomList<int>() { 1, 2, 3 };

            CustomNode<int> node = new CustomNode<int>(4);
            list.Add(node);

            var index = list.FindIndex(node);

            Assert.Equal(list.Count - 1, index);
        }

        [Fact]
        public void CopyToTest()
        {
            CustomList<string> list = new CustomList<string>() { "A", "B", "C" };
            var array = new string[list.Count];

            list.CopyTo(array, list.Count - 1);

            Assert.Equal(string.Join("", list), string.Join("", array));
        }

        [Fact]
        public void ClearTest()
        {
            CustomList<string> list = new CustomList<string> { "CLR", "via", "C#" };

            list.Clear();

            Assert.Empty(list);
        }

        #endregion
    }
}
