using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Tree;
using System.Linq;

namespace BinaryTreeTests
{
    public class BinaryTreeTests
    {
        int[] numbers = { 100, 60, 400, 10, 90 };


        [Fact]
        public void BinaryTree_TraversePreOrder_ShouldReturnCollectionInRightOrder()
        {
            //arrange
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);
            int[] expected = { 100, 60, 10, 90, 400 };

            //act
            var actual = tree.Traverse(Traversal.PreOrder).ToList();

            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BinaryTree_TraverseInOrder_ShouldReturnCollectionInRightOrder()
        {
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);
            int[] expected = { 10, 60, 90, 100, 400 };

            var actual = tree.Traverse(Traversal.InOrder).ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BinaryTree_TraverseOutOrder_ShouldReturnCollectionInRightOrder()
        {
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);
            int[] expected = { 10, 90, 60, 400, 100 };

            var actual = tree.Traverse(Traversal.OutOrder).ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BinaryTree_RemoveLeaf_ShouldReturnTrue()
        {
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);
            var expected = true;

            var actual = tree.Remove(10);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BinaryTree_RemoveUnexistingValue_ShouldReturnFalse()
        {
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);
            var expected = false;

            var actual = tree.Remove(20);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BinaryTree_RemoveLeaf_ComparingCollectionsAfterRemovingLeaf()
        {
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);
            int[] expected = { 60, 90, 100, 400 };
            

            tree.Remove(10);
            var actual = tree.Traverse(Traversal.InOrder).ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BinaryTree_RemoveNodeWithChild_ComparingCollectionsAfterRemoving()
        {
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);
            int[] expected = { 90, 100, 400 };


            tree.Remove(10);
            tree.Remove(60);
            var actual = tree.Traverse(Traversal.InOrder).ToList();

            Assert.Equal(expected, actual);
        }

        

        [Theory]
        [InlineData(10, true)]
        [InlineData(400, true)]
        [InlineData(100, true)]
        [InlineData(60, true)]
        [InlineData(35, false)]
        [InlineData(11, false)]
        public void BinaryTree_Search_ShouldReturnTrueIfValueIsInTree(int value, bool expected)
        {
            var tree = new BinaryTree<int>();
            foreach (var number in numbers)
                tree.Add(number);

            var actual = tree.Search(value);

            Assert.Equal(expected, actual);
        }
    }
}
